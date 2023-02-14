using Microsoft.UI.Xaml.Controls;
using NoobNotFound.WinUI.Common.Helpers.Tenor;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace NoobNotFound.WinUI.Common.UI.Controls
{
    public sealed partial class TenorFlyout : Flyout
    {
        public event EventHandler<Helpers.Tenor.JSON.SearchResult.Result>? ItemInvoked;
        public string APIKey { get; set; }
        public string ClientKey { get; set; }
        public TenorClient TenorClient { get; private set; }
        private ObservableCollection<Models.TenorGIF> _GIFs;
        private ObservableCollection<Models.TenorGIF> GIFs
        {
            get => _GIFs ??= new ObservableCollection<Models.TenorGIF>();
            set
            {
                _GIFs = value;
              ir.ItemsSource = null;
              ir.ItemsSource = GIFs;
            }
        }
        public TenorFlyout()
        {
            this.InitializeComponent();
            this.Opened += (_, _) =>
            {
                TenorClient = new(APIKey, ClientKey);
                txtSearch_TextChanged(null, null);
            };
            this.Closed += (_, _) =>
            {
                TenorClient.Dispose();
            };
        }

        private async void txtSearch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs e)
        {
            SPError.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            pbLoad.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            var d = string.IsNullOrEmpty(txtSearch.Text) ? await TenorClient.GetTrendings() : await TenorClient.Search(txtSearch.Text, 30);
            pbLoad.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;

            if (d != null)
            {
                GIFs = new ObservableCollection<Models.TenorGIF>(d.results.Select(x => new Models.TenorGIF() { ShowLoad = true, SearchTerm = txtSearch.Text, ImageSource = x.media_formats.tinygif.url, JSON = x, Tags = x.tags }));
            }
            else
            {
                GIFs.Clear();
                SPError.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            }
        }

        private void Image_ImageOpened(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                ((Models.TenorGIF)(sender as Image).DataContext).ShowLoad = false;
            }
            catch { }
        }

        private async void ir_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itm = ((Models.TenorGIF)e.ClickedItem);
            ItemInvoked?.Invoke(this, itm.JSON);
            pbLoad.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            await TenorClient.RegisterShare(itm.JSON.id, itm.SearchTerm);
            txtSearch.Text = "";
            this.Hide();
            pbLoad.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        }
    }
}