﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NoobSharp.Common.WinUI.Enums;

namespace NoobSharp.Common.WinUI.Converters
{
    #region ExpanderTemplateSelector
    public class ExpanderTemplateSelector : DataTemplateSelector
    {
        public ExpanderStyles Style { get; set; }
        public DataTemplate Default { get; set; }
        public DataTemplate Static { get; set; }
        public DataTemplate Button { get; set; }
        public DataTemplate Transparent { get; set; }
        public DataTemplate Disabled { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            return Style switch
            {
                ExpanderStyles.Static => Static,
                ExpanderStyles.Button => Button,
                ExpanderStyles.Transparent => Transparent,
                ExpanderStyles.Disabled => Disabled,
                _ => Default,
            };
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return Style switch
            {
                ExpanderStyles.Static => Static,
                ExpanderStyles.Button => Button,
                ExpanderStyles.Transparent => Transparent,
                ExpanderStyles.Disabled => Disabled,
                _ => Default,
            };
        }
    }
    // https://social.msdn.microsoft.com/Forums/windowsapps/en-US/b2e0f948-df35-49da-a70f-1892205b8570/contenttemplateselector-datatemplateselectorselecttemplatecore-item-parameter-is-always-null?forum=winappswithcsharp
    #endregion
}
