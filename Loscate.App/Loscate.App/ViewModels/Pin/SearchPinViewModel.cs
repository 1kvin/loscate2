using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loscate.App.ApiRequests.Map;
using Loscate.App.Map;
using Loscate.App.Repository;
using Loscate.App.Services;
using Loscate.App.Views;
using Xamarin.Forms;

namespace Loscate.App.ViewModels
{
    public class SearchPinViewModel : BaseViewModel
    {
        public string SearchText { get; set; }
        public Command<CustomPin> ItemTapped { get; }
        public Command LoadItemsCommand { get; }
        public ObservableCollection<CustomPin> Items { get; }

        private CustomPin _selectedItem;
        private readonly IFirebaseAuthenticator firebaseAuthenticator;

        public SearchPinViewModel()
        {
            firebaseAuthenticator = DependencyService.Get<IFirebaseAuthenticator>();
            Items = new ObservableCollection<CustomPin>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<CustomPin>(OnItemSelected);
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var res = await MapRequests.SearchPin(firebaseAuthenticator, SearchText).ConfigureAwait(true);
                var items = res.Select(p => p.ConvertPin()).ToList();

                foreach (var item in items)
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