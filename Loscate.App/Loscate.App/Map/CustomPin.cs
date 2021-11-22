using Xamarin.Forms.Maps;

namespace Loscate.App.Map
{
    public class CustomPin : Pin
    {
        public int PinId { get; set; }
        public string UserUID { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Photo { get; set; }
    }
    
    public static class CustomPinExt
    {
        public static CustomPin ConvertPin(this DTO.Map.Pin pin)
        {
            return new CustomPin()
            {
                Position = new Position(pin.Latitude, pin.Longitude),
                ShortName = pin.ShortName,
                FullName = pin.FullName,
                Photo = pin.PhotoBase64,
                Label = pin.ShortName,
                UserUID = pin.UserUID,
                PinId = pin.Id
            };
        }
    }
}