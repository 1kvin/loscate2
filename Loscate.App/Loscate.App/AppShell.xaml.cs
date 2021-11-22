using Loscate.App.Repository;
using Loscate.App.Services;
using Loscate.App.ViewModels;
using Loscate.App.Views;
using Loscate.DTO.Firebase;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using Loscate.App.Models;
using Xamarin.Forms;

namespace Loscate.App
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell(FirebaseUser user)
        {
            var userRepository = new UserRepository();
            var pinsRepository = new PinsRepository();

            userRepository.user = new XamarinUser(user);
            pinsRepository.Pins = new List<Map.CustomPin>();

            var container = TinyIoCContainer.Current.Register(userRepository);
            var pinsContainer = TinyIoCContainer.Current.Register(pinsRepository);

            InitializeComponent();
            Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
            Routing.RegisterRoute(nameof(MyProfilePage), typeof(MyProfilePage));
            Routing.RegisterRoute(nameof(DialogsPage), typeof(DialogsPage));
            Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage));
            Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));
            Routing.RegisterRoute(nameof(MyPinsPage), typeof(MyPinsPage));
            Routing.RegisterRoute(nameof(PinDetailPage), typeof(PinDetailPage));
            Routing.RegisterRoute(nameof(SearchPinPage), typeof(SearchPinPage));
            Routing.RegisterRoute(nameof(AddPinPage), typeof(AddPinPage));
        }
    }
}
