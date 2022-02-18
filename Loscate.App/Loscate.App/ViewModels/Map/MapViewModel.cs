using Loscate.App.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Loscate.App.ApiRequests.Map;
using Loscate.App.Map;
using Loscate.App.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using Loscate.App.Repository;
using System;
using Nancy.TinyIoc;

namespace Loscate.App.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        public Command AddPinCommand { get; }
        public Command SearchCommand { get; }
        public Command RefreshCommand { get; }

        private readonly CustomMap map;
        private readonly IFirebaseAuthenticator firebaseAuthenticator;
        private readonly PinsRepository pinsRepository;
        private IMapService mapService;
        private Position startPosition = new Position(60.01001948251575, 30.374142388879566); //spbstu

        private List<CustomPin> pins = new List<CustomPin>();

        public MapViewModel(CustomMap map)
        {
            this.map = map;
            mapService = DependencyService.Get<IMapService>();
            firebaseAuthenticator = DependencyService.Get<IFirebaseAuthenticator>();
            pinsRepository = TinyIoCContainer.Current.Resolve<PinsRepository>();
            mapService.OnPinClickSubscribe(OnPinClick);
            SearchCommand = new Command(async () => await OpenSearchPage());
            AddPinCommand = new Command(async () => await OpenAddPinPage());
            RefreshCommand = new Command(async () => await LoadPins());

            map.MoveToRegion(MapSpan.FromCenterAndRadius(startPosition, Distance.FromMiles(1.0)));
            map.CustomPins = pins;

            RefreshCommand.Execute(this);

            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                Task.Run(async () => await LoadPins());
                return true;
            });
        }

        public async Task LoadPins()
        {
            var res = await MapRequests.GetPins(firebaseAuthenticator).ConfigureAwait(true);
            var customPins = res.Select(p=>p.ConvertPin()).ToList();

            if (customPins.Count != pins.Count)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    map.Pins.Clear();
                    pins.Clear();

                    pins.AddRange(customPins);
                    customPins.ForEach(p => map.Pins.Add(p));
                });
            }
            else
            {
                foreach (var customPin in customPins)
                {
                    bool findFlag = false;
                    foreach (var mapPin in pins)
                    {
                        if (mapPin.ShortName == customPin.ShortName)
                        {
                            findFlag = true;
                            break;
                        }
                    }

                    if (!findFlag)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            map.Pins.Clear();
                            pins.Clear();

                            pins.AddRange(customPins);
                            customPins.ForEach(p => map.Pins.Add(p));
                        });
                        break;
                    }
                }
            }
        }

        private async Task OpenAddPinPage()
        {
            await Shell.Current.GoToAsync($"{nameof(AddPinPage)}");
        }

        private async Task OpenSearchPage()
        {
            await Shell.Current.GoToAsync($"{nameof(SearchPinPage)}");
        }

        private void OnPinClick(CustomPin pin)
        {
            PhotoBase64DTO.PhotoBase64 = pin.Photo;

            Shell.Current.GoToAsync($"{nameof(PinDetailPage)}?" +
                $"{nameof(PinDetailViewModel.ShortName)}={pin.ShortName}&" +
                $"{nameof(PinDetailViewModel.FullName)}={pin.FullName}&" +
                $"{nameof(PinDetailViewModel.PinId)}={pin.PinId}&" +
                $"{nameof(PinDetailViewModel.UserUID)}={pin.UserUID}");
        }
    }
}