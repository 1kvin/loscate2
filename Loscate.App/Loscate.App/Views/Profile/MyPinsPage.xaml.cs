using Loscate.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Loscate.App.Views
{
    [ExcludeFromCodeCoverage]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPinsPage : ContentPage
    {
        private MyPinsViewModel myPinsViewModel;
        public MyPinsPage()
        {
            InitializeComponent();
            this.BindingContext = myPinsViewModel = new MyPinsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            myPinsViewModel.OnAppearing();
        }
    }
}