using System;
using Loscate.App.Map;
using Loscate.App.Services;

namespace Loscate.App.UnitTests
{
    public class FakeMap : IMapService
    {
        public void OnPinClickSubscribe(Action<CustomPin> action)
        {
            
        }

        public void OnPinClickUnSubscribe(Action<CustomPin> action)
        {
            
        }
    }
}