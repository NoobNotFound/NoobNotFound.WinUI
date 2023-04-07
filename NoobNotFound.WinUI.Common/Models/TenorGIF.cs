using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NoobSharp.Common.WinUI.Models
{
    internal partial class TenorGIF : ObservableObject
    {
        [ObservableProperty]
        private string _ImageSource;

        [ObservableProperty]
        private bool _ShowLoad;

        [ObservableProperty]
        private List<string> _Tags;

        [ObservableProperty]
        private string _SearchTerm;

        [ObservableProperty]
        private Helpers.Tenor.JSON.SearchResult.Result _JSON;

        public string TagsString => string.Join(", ", Tags);
    }
}
