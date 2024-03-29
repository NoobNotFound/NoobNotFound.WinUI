﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;
using NoobSharp.Common.WinUI.ViewModels;
using NoobSharp.Common.WinUI.Enums;

namespace NoobSharp.Common.WinUI.UI.Controls
{
    [ContentProperty(Name = "Controls")]
    public sealed partial class Expander : UserControl
    {
        private readonly ExpanderViewModel VM = new();
        public Expander()
        {
            this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new("ms-appx:///NoobSharp.Common.WinUI/ResourceDictionaries/RightAlignedToggleSwitch.xaml") });
            this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new("ms-appx:///NoobSharp.Common.WinUI/ResourceDictionaries/Expanders.xaml") });
            this.InitializeComponent();
        }
        public event RoutedEventHandler Click;
        /// <summary>
        /// <inheritdoc cref="ExpanderViewModel.Title"/>
        /// </summary>
        public string Title
        {
            get => VM.Title;
            set => VM.Title = value;
        }

        /// <summary>
        /// <inheritdoc cref="ExpanderViewModel.Description"/>
        /// </summary>
        public string Description
        {
            get => VM.Description;
            set => VM.Description = value;
        }

        /// <summary>
        /// <inheritdoc cref="ExpanderViewModel.ExpanderStyle"/>
        /// </summary>
        public ExpanderStyles ExpanderStyle
        {
            get => VM.ExpanderStyle;
            set => VM.ExpanderStyle = value;
        }

        /// <summary>
        /// <inheritdoc cref="ExpanderViewModel.Icon"/>
        /// </summary>
        public string Icon
        {
            get => VM.Icon;
            set => VM.Icon = value;
        }

        /// <summary>
        /// <inheritdoc cref="ExpanderViewModel.Controls"/>
        /// </summary>
        public object Controls
        {
            get => VM.Controls;
            set => VM.Controls = value;
        }

        /// <summary>
        /// <inheritdoc cref="ExpanderViewModel.HeaderControls"/>
        /// </summary>
        public object HeaderControls
        {
            get => VM.HeaderControls;
            set => VM.HeaderControls = value;
        }

        /// <summary>
        /// <inheritdoc cref="ExpanderViewModel.TitleFontSize"/>
        /// </summary>
        public int TitleFontSize
        {
            get => VM.TitleFontSize;
            set => VM.TitleFontSize = value;
        }

        /// <summary>
        /// <inheritdoc cref="ExpanderViewModel.IconFontSize"/>
        /// </summary>
        public int IconFontSize
        {
            get => VM.IconFontSize;
            set => VM.IconFontSize = value;
        }

        /// <summary>
        /// <inheritdoc cref="ExpanderViewModel.DescriptionFontSize"/>
        /// </summary>
        public int DescriptionFontSize
        {
            get => VM.DescriptionFontSize;
            set => VM.DescriptionFontSize = value;
        }
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}
