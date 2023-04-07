using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NoobSharp.Common.WinUI.UI.Controls
{
    public sealed partial class TitleBar : UserControl
    {
        public string Title { get; set; }
        public string Release { get; set; }
        public ImageSource IconSource { get; set; }
        public TitleBar()
        {
            this.InitializeComponent();
        }
    }
}
