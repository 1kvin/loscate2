using Loscate.App.Repository;
using Loscate.App.Services;
using Loscate.App.Views;
using Loscate.DTO.Firebase;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Text;
using Loscate.App.Models;
using Xamarin.Forms;
using System.IO;
using System.Threading.Tasks;
using Loscate.App.ApiRequests.User;

namespace Loscate.App.ViewModels
{
    public class MyProfileViewModel : BaseViewModel
    {
        public UserRepository UserRepository { get; set; }

        private IFirebaseAuthenticator firebaseAuth;
        public Command SignOutCommand { get; }
        public Command EditAccountCommand { get; }
        public Command OpenMyPinsCommand { get; }
        public ImageSource UserImage { get; set; }
        public string UserName { get; set; }


        public MyProfileViewModel()
        {
            UserRepository = TinyIoCContainer.Current.Resolve<UserRepository>();
            SignOutCommand = new Command(SignOut);
            EditAccountCommand = new Command(EditAccount);
            OpenMyPinsCommand = new Command(OpenMyPins);
            
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                UpdateProfile();
                return true;
            });
            
            UpdateProfile();
        }

        private void UpdateProfile()
        {
            UserImage = UserRepository.user.UserImage;
            UserName = UserRepository.user.Name;
        }

        private async void EditAccount()
        {
            await Shell.Current.GoToAsync($"{nameof(EditProfilePage)}");
        }

        private async void OpenMyPins()
        {
            await Shell.Current.GoToAsync($"{nameof(MyPinsPage)}");
        }

        private void SignOut()
        {
            firebaseAuth = DependencyService.Get<IFirebaseAuthenticator>();

            firebaseAuth.SubscribeToTokenUpdate(TokenUpdate);
            firebaseAuth.SignOut();
        }

        private void TokenUpdate()
        {
            if (string.IsNullOrEmpty(firebaseAuth.GetAuthToken()))
            {
                firebaseAuth.UnSubscribeToTokenUpdate(TokenUpdate);
                Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Ошибка", "Ошибка выхода", "OK");
            }
        }
    }
}