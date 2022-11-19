using Microsoft.UI.Xaml.Controls;
using NoobNotFound.WinUI.Common.Helpers.Tenor;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System;

namespace NoobNotFound.WinUI.Common.UI.Controls
{
    public sealed partial class TenorFlyout : Flyout, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
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
                PropertyChanged?.Invoke(this, new(null));
            }
        }
        public TenorFlyout()
        {
            this.InitializeComponent();
            this.Opened += (_, _) =>
            {
                TenorClient = new(APIKey, ClientKey);
            };
            this.Closed += (_, _) =>
            {
                TenorClient.Dispose();
            };
        }

        private async void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itm = ((Models.TenorGIF)e.ClickedItem);
            ItemInvoked.Invoke(this,itm.JSON);
            await TenorClient.RegisterShare(itm.JSON.id);
            this.Hide();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}