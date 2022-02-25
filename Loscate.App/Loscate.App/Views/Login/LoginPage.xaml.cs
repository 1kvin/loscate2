using System.Diagnostics.CodeAnalysis;
using Loscate.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Loscate.App.Views
{
    [ExcludeFromCodeCoverage]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
    }
}