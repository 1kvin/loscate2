using Loscate.App.Services;
using Loscate.DTO.Map;
using System;
using System.Diagnostics.CodeAnalysis;
using Loscate.App.Map;
using Xamarin.Forms;

[assembly: Dependency(typeof(Loscate.App.Droid.Map.MapService))]
namespace Loscate.App.Droid.Map
{
    [ExcludeFromCodeCoverage]
    public class MapService : IMapService
    {
        private Action<CustomPin> OnPinClickActions;

        public void Invoke(CustomPin pin)
        {
            OnPinClickActions?.Invoke(pin);
        }

        public void OnPinClickSubscribe(Action<CustomPin> action)
        {
            OnPinClickActions += action;
        }

        public void OnPinClickUnSubscribe(Action<CustomPin> action)
        {
            OnPinClickActions -= action;
        }
    }
}