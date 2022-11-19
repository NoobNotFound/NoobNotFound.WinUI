using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NoobNotFound.WinUI.Common.Models
{
    internal class TenorGIF : ObservableObject
    {
        private string _ImageSource;

        public string ImageSource
        {
            get => _ImageSource;
            set => SetProperty(ref _ImageSource, value);
        }
        private List<string> _Tags;

        public List<string> Tags
        {
            get => _Tags;
            set => SetProperty(ref _Tags, value);
        }

        public string TagsString
        {
            get => string.Join(", ", Tags);            
        }

        public Helpers.Tenor.JSON.SearchResult.Result JSON { get; set; }
    }
}
