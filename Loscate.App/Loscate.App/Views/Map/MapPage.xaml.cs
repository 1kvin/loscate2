using Loscate.App.Map;
using Loscate.App.Models;
using Loscate.App.Services;
using Loscate.App.ViewModels;
using Loscate.DTO.Map;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Loscate.App.Views
{
    [ExcludeFromCodeCoverage]
    public partial class MapPage : ContentPage
    {
        private IMapService mapService;

        public MapPage()
        {
            InitializeComponent();
            this.BindingContext = new MapViewModel(Map);
        }
    }
}