using System.Diagnostics.CodeAnalysis;
using Loscate.App.Services;
using Loscate.App.ViewModels;
using Loscate.DTO.Firebase;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Loscate.App.Views
{
    [ExcludeFromCodeCoverage]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyProfilePage : ContentPage
    {
        public MyProfilePage()
        {
            InitializeComponent();
            this.BindingContext = new MyProfileViewModel();
        }
    }
}