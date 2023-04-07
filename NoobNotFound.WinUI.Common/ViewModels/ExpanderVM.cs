using NoobSharp.Common.WinUI.Enums;

namespace NoobSharp.Common.WinUI.ViewModels
{
    internal class ExpanderViewModel : ViewModel
    {
        private ExpanderStyles _expanderStyle;
        /// <summary>
        /// Gets or sets the style for the expander.
        /// </summary>
        public ExpanderStyles ExpanderStyle
        {
            get => _expanderStyle;
            set => Set(ref _expanderStyle, value);
        }

        private string _title;
        /// <summary>
        /// Gets or sets the title for the expander.
        /// </summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private string _description;
        /// <summary>
        /// Gets or sets the description for the expander.
        /// </summary>
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        private string _icon;
        /// <summary>
        /// Gets or sets the icon for the expander as a glyph.
        /// </summary>
        public string Icon
        {
            get => _icon;
            set => Set(ref _icon, value);
        }


        private int _titleFontSize = 14;
        /// <summary>
        /// Gets or sets the Fontsize for the expander Title.
        /// </summary>
        public int TitleFontSize
        {
            get => _titleFontSize;
            set => Set(ref _titleFontSize, value);
        }

        private int _iconFontSize = 14;
        /// <summary>
        /// Gets or sets the Fontsize for the expander Title.
        /// </summary>
        public int IconFontSize
        {
            get => _iconFontSize;
            set => Set(ref _iconFontSize, value);
        }

        private int _DescriptionFontSize = 12;
        /// <summary>
        /// Gets or sets the Fontsize for the expander Title.
        /// </summary>
        public int DescriptionFontSize
        {
            get => _DescriptionFontSize;
            set => Set(ref _DescriptionFontSize, value);
        }
        private object _controls;
        /// <summary>
        /// Gets or sets the content for the expander. This is
        /// displayed to the left of most expander styles, but
        /// the default one uses <see cref="HeaderControls"/>
        /// for that purpose.
        /// </summary>
        public object Controls
        {
            get => _controls;
            set => Set(ref _controls, value);
        }

        private object _headerControls;
        /// <summary>
        /// Gets or sets the header content for the default
        /// expander.
        /// </summary>
        public object HeaderControls
        {
            get => _headerControls;
            set => Set(ref _headerControls, value);
        }

    }
}
