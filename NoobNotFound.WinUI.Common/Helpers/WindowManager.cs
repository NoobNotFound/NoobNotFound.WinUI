using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using PInvoke;
using System;
using System.Runtime.InteropServices;
using Windows.ApplicationModel;
using WinRT;
using WinRT.Interop;

namespace NoobSharp.Common.WinUI.Helpers
{
    public static class WindowManager
    {
        /// <summary>
        /// Add mica and the icon to the <paramref name="window"/>
        /// </summary>
        public static void IntializeWindow(this Window window,Windows.Storage.StorageFile iconFile, MicaKind micakind = MicaKind.Base)
        {
            var icon = User32.LoadImage(
                hInst: IntPtr.Zero,
                name: iconFile.Path.ToCharArray(),
                type: User32.ImageType.IMAGE_ICON,
                cx: 0,
                cy: 0,
                fuLoad: User32.LoadImageFlags.LR_LOADFROMFILE | User32.LoadImageFlags.LR_DEFAULTSIZE | User32.LoadImageFlags.LR_SHARED
            );
            var Handle = WindowNative.GetWindowHandle(window);
            User32.SendMessage(Handle, User32.WindowMessage.WM_SETICON, (IntPtr)1, icon);
            User32.SendMessage(Handle, User32.WindowMessage.WM_SETICON, (IntPtr)0, icon);
            new MicaBackground(window,micakind).TrySetMicaBackdrop();
        }

        /// <summary>
        /// Sets the customized titlebar if supported
        /// </summary>
        /// <exception cref="NullReferenceException"/>
        public static void SetExtendedTitleBar(this Window window, UIElement AppTitleBar = null)
        {
            FrameworkElement RootUI = (FrameworkElement)window.Content;
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                AppWindow AppWindow = window.ToAppWindow();
                var titlebar = AppWindow.TitleBar;
                titlebar.ExtendsContentIntoTitleBar = true;
                void SetColor(ElementTheme acualTheme)
                {
                    titlebar.ButtonBackgroundColor = titlebar.ButtonInactiveBackgroundColor = titlebar.ButtonPressedBackgroundColor = Colors.Transparent;
                    switch (acualTheme)
                    {
                        case ElementTheme.Dark:
                            titlebar.ButtonHoverBackgroundColor = Colors.Black;
                            titlebar.ButtonForegroundColor = Colors.White;
                            titlebar.ButtonHoverForegroundColor = Colors.White;
                            titlebar.ButtonPressedForegroundColor = Colors.Silver;
                            break;
                        case ElementTheme.Light:
                            titlebar.ButtonHoverBackgroundColor = Colors.White;
                            titlebar.ButtonForegroundColor = Colors.Black;
                            titlebar.ButtonHoverForegroundColor = Colors.Black;
                            titlebar.ButtonPressedForegroundColor = Colors.DarkGray;
                            break;
                    }
                }
                RootUI.ActualThemeChanged += (s, _) => SetColor(s.ActualTheme);
                if (AppTitleBar != null)
                {
                    window.SetTitleBar(AppTitleBar);
                }
                SetColor(RootUI.ActualTheme);
            }
            else
            {
                window.ExtendsContentIntoTitleBar = true;
                if (AppTitleBar != null)
                {
                    window.SetTitleBar(AppTitleBar);
                }
            }
        }

        [DllImport("Shcore.dll", SetLastError = true)]
        internal static extern int GetDpiForMonitor(IntPtr hmonitor, Monitor_DPI_Type dpiType, out uint dpiX, out uint dpiY);

        internal enum Monitor_DPI_Type : int
        {
            MDT_Effective_DPI = 0,
            MDT_Angular_DPI = 1,
            MDT_Raw_DPI = 2,
            MDT_Default = MDT_Effective_DPI
        }
        public static double GetScaleAdjustment(this Window window)
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(window);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            DisplayArea displayArea = DisplayArea.GetFromWindowId(wndId, DisplayAreaFallback.Primary);
            IntPtr hMonitor = Win32Interop.GetMonitorFromDisplayId(displayArea.DisplayId);

            // Get DPI.
            int result = GetDpiForMonitor(hMonitor, Monitor_DPI_Type.MDT_Default, out uint dpiX, out uint _);
            if (result != 0)
            {
                throw new Exception("Could not get DPI for monitor.");
            }

            uint scaleFactorPercent = (uint)(((long)dpiX * 100 + (96 >> 1)) / 96);
            return scaleFactorPercent / 100.0;
        }
    }
    public class WindowsSystemDispatcherQueueHelper
    {
        private object? _dispatcherQueueController;

        [StructLayout(LayoutKind.Sequential)]
        internal struct DispatcherQueueOptions
        {
            internal int dwSize;
            internal int threadType;
            internal int apartmentType;
        }

        [DllImport("CoreMessaging.dll")]
        private static extern int CreateDispatcherQueueController([In] DispatcherQueueOptions options, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object? dispatcherQueueController);

        public void EnsureWindowsSystemDispatcherQueueController()
        {
            if (Windows.System.DispatcherQueue.GetForCurrentThread() != null)
            {
                // one already exists, so we'll just use it.
                return;
            }

            if (_dispatcherQueueController == null)
            {
                DispatcherQueueOptions options;
                options.dwSize = Marshal.SizeOf(typeof(DispatcherQueueOptions));
                options.threadType = 2;
                options.apartmentType = 2;

                CreateDispatcherQueueController(options, ref _dispatcherQueueController);
            }
        }
    }
    public class MicaBackground
    {
        private readonly Window _window;
        private MicaController _micaController;
        private SystemBackdropConfiguration _backdropConfiguration = new();
        private readonly WindowsSystemDispatcherQueueHelper _dispatcherQueueHelper = new();

        public MicaBackground(Window window,MicaKind kind)
        {
            _micaController = new() { Kind= kind };
            _window = window;
        }

        public bool TrySetMicaBackdrop()
        {
            if (MicaController.IsSupported())
            {
                _dispatcherQueueHelper.EnsureWindowsSystemDispatcherQueueController();
                _window.Activated += WindowOnActivated;
                _window.Closed += WindowOnClosed;
                ((FrameworkElement)_window.Content).ActualThemeChanged += MicaBackground_ActualThemeChanged;
                _backdropConfiguration.IsInputActive = true;
                _backdropConfiguration.Theme = _window.Content switch
                {
                    FrameworkElement { ActualTheme: ElementTheme.Dark } => SystemBackdropTheme.Dark,
                    FrameworkElement { ActualTheme: ElementTheme.Light } => SystemBackdropTheme.Light,
                    FrameworkElement { ActualTheme: ElementTheme.Default } => SystemBackdropTheme.Default,
                    _ => throw new InvalidOperationException("Unknown theme")
                };

                _micaController.AddSystemBackdropTarget(_window.As<ICompositionSupportsSystemBackdrop>());
                _micaController.SetSystemBackdropConfiguration(_backdropConfiguration);
                return true;
            }

            return false;
        }

        private void MicaBackground_ActualThemeChanged(FrameworkElement sender, object args)
        {
            if (_backdropConfiguration != null)
            {
                SetConfigurationSourceTheme();
            }

        }
        private void SetConfigurationSourceTheme()
        {
            switch (((FrameworkElement)_window.Content).ActualTheme)
            {
                case ElementTheme.Dark: _backdropConfiguration.Theme = SystemBackdropTheme.Dark; break;
                case ElementTheme.Light: _backdropConfiguration.Theme = SystemBackdropTheme.Light; break;
                case ElementTheme.Default: _backdropConfiguration.Theme = SystemBackdropTheme.Default; break;
            }
        }
        private void WindowOnClosed(object sender, WindowEventArgs args)
        {
            _micaController.Dispose();
            _micaController = null!;
            _window.Activated -= WindowOnActivated;
            _backdropConfiguration = null!;
        }

        private void WindowOnActivated(object sender, WindowActivatedEventArgs args)
        {
            _backdropConfiguration.IsInputActive = args.WindowActivationState is not WindowActivationState.Deactivated;
        }
    }
}
