using Loscate.App.ApiRequests.Map;
using Loscate.App.Map;
using Loscate.App.Repository;
using Loscate.App.Services;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loscate.App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Loscate.App.ViewModels
{
    public class MyPinsViewModel : BaseViewModel
    {
        public Command<CustomPin> ItemTapped { get; }
        public Command LoadItemsCommand { get; }
        public ObservableCollection<CustomPin> Items { get; }

        private CustomPin _selectedItem;
        private readonly UserRepository userRepository;
        private readonly IFirebaseAuthenticator firebaseAuthenticator;

        public MyPinsViewModel()
        {
            userRepository = TinyIoCContainer.Current.Resolve<UserRepository>();
            firebaseAuthenticator = DependencyService.Get<IFirebaseAuthenticator>();
            Items = new ObservableCollection<CustomPin>();
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);

            ItemTapped = new Command<CustomPin>(OnItemSelected);
        }

        private void ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var res = MapRequests.GetPins(firebaseAuthenticator).Result;
                var items = res.Select(p=>p.ConvertPin()).ToList();

                foreach (var item in items.Where(item => item.UserUID == userRepository.user.UID))
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public CustomPin SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(CustomPin item)
        {
            if (item == null)
                return;

            PhotoBase64DTO.PhotoBase64 = item.Photo;

            await Shell.Current.GoToAsync($"{nameof(PinDetailPage)}?" +
                                          $"{nameof(PinDetailViewModel.ShortName)}={item.ShortName}&" +
                                          $"{nameof(PinDetailViewModel.FullName)}={item.FullName}&" +
                                          $"{nameof(PinDetailViewModel.UserUID)}={item.UserUID}&" +
                                          $"{nameof(PinDetailViewModel.PinId)}={item.PinId}");
        }
    }
}