using NoobNotFound.WinUI.Common.Enums;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Threading.Tasks;

namespace NoobNotFound.WinUI.Common.Helpers
{
    /// <summary>
    /// A <see cref="ContentDialog"/> as a MessageBox...<br/>
    /// </summary>
    public class MessageBox : ContentDialog
    {
        public static XamlRoot MainXamlRoot { get; set; }
        public static ElementTheme Theme { get; set; } = ElementTheme.Default;
        public MessageBoxResults? Result { get; set; } = null;
        public MessageBox(string title, string caption, MessageBoxButtons buttons, string cusbtn1 = null, string cusbtn2 = null)
        {
            Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            Title = title;
            this.Content = new CommunityToolkit.WinUI.UI.Controls.MarkdownTextBlock() { Text = caption, Background = new SolidColorBrush(Colors.Transparent) };
            if (buttons == MessageBoxButtons.Ok)
            {
                PrimaryButtonText = "";
                SecondaryButtonText ="OK";
                DefaultButton = ContentDialogButton.None;
            }
            else if (buttons == MessageBoxButtons.OkCancel)
            {
                PrimaryButtonText ="OK";
                SecondaryButtonText = "Cancel";
                DefaultButton = ContentDialogButton.Primary;
            }
            else if (buttons == MessageBoxButtons.YesNo)
            {
                PrimaryButtonText = "Yes";
                SecondaryButtonText = "No";
                DefaultButton = ContentDialogButton.Primary;
            }
            else if (buttons == MessageBoxButtons.Custom)
            {
                if (!string.IsNullOrEmpty(cusbtn1))
                {
                    PrimaryButtonText = cusbtn1;
                }
                if (!string.IsNullOrEmpty(cusbtn2))
                {
                    SecondaryButtonText = cusbtn2;
                }
                if (string.IsNullOrEmpty(cusbtn2) && string.IsNullOrEmpty(cusbtn1))
                {
                    PrimaryButtonText = "Yes";
                    SecondaryButtonText = "No";
                    DefaultButton = ContentDialogButton.Primary;
                }
            }
            else if (buttons == MessageBoxButtons.CustomWithCancel)
            {
                if (!string.IsNullOrEmpty(cusbtn1))
                {
                    PrimaryButtonText = cusbtn1;
                }
                if (!string.IsNullOrEmpty(cusbtn2))
                {
                    SecondaryButtonText = cusbtn2;
                }
                if (string.IsNullOrEmpty(cusbtn2) && string.IsNullOrEmpty(cusbtn1))
                {
                    DefaultButton = ContentDialogButton.Primary;
                    PrimaryButtonText = "Yes";
                    SecondaryButtonText = "No";
                }
                CloseButtonText = "Cancel";
            }
            PrimaryButtonClick += ContentDialog_PrimaryButtonClick;
            SecondaryButtonClick += ContentDialog_SecondaryButtonClick;
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (sender.PrimaryButtonText =="OK")
            {
                Result = MessageBoxResults.Ok;
            }
            else if (sender.PrimaryButtonText == "Yes")
            {
                Result = MessageBoxResults.Yes;
            }
            else
            {
                Result = MessageBoxResults.CustomResult1;
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (sender.SecondaryButtonText =="OK")
            {
                Result = MessageBoxResults.Ok;
            }
            else if (sender.SecondaryButtonText == "Cancel")
            {
                Result = MessageBoxResults.Cancel;
            }
            else if (sender.SecondaryButtonText == "No")
            {
                Result = MessageBoxResults.No;
            }
            else
            {
                Result = MessageBoxResults.CustomResult2;
            }
        }
        public static async Task<MessageBoxResults> Show(string title, string caption, MessageBoxButtons buttons, string customResult1 = null, string customResult2 = null, XamlRoot root = null)
        {
            var d = new MessageBox(title, caption, buttons, customResult1, customResult2)
            {
                XamlRoot = root ?? MainXamlRoot,
                RequestedTheme = Theme
            };
            try
            {
                await d.ShowAsync();
            }
            catch
            {
                throw new NotSupportedException("Cannot show 2 or more dialogs once");
            }
            return d.Result == null ? MessageBoxResults.Cancel : d.Result.Value;
        }
        public static async Task<MessageBoxResults> Show(string text, XamlRoot root = null)
        {
            var d = new MessageBox("Information", text, MessageBoxButtons.Ok)
            {
                XamlRoot = root ?? MainXamlRoot,
                RequestedTheme = Theme
            };
            try
            {
                await d.ShowAsync();
            }
            catch
            {
                throw new NotSupportedException("Cannot show 2 or more dialogs once");
            }
            return d.Result == null ? MessageBoxResults.Cancel : d.Result.Value;
        }
    }
}
