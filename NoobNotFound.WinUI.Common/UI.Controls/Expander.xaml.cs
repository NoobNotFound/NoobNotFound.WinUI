﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;
using NoobNotFound.WinUI.Common.ViewModels;
using NoobNotFound.WinUI.Common.Enums;

namespace NoobNotFound.WinUI.Common.UI.Controls
{
    [ContentProperty(Name = "Controls")]
    public sealed partial class Expander : UserControl
    {
        private readonly ExpanderViewModel VM = new();
        public Expander()
        {
            this.InitializeComponent();
           // this.Loaded += (_,_) =>
            //this.Content = new Helpers.CompositionControl { ContentTemplateSelector = ExpanderSelector, Content = VM };
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
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}
