using Loscate.App.Repository;
using Loscate.App.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using Loscate.App.ApiRequests.Map;
using Loscate.App.ApiRequests.Social.Message;
using Loscate.App.Views;
using Nancy.TinyIoc;
using Xamarin.Forms;

namespace Loscate.App.ViewModels
{
    [QueryProperty(nameof(ShortName), nameof(ShortName))]
    [QueryProperty(nameof(FullName), nameof(FullName))]
    [QueryProperty(nameof(UserUID), nameof(UserUID))]
    [QueryProperty(nameof(PinId), nameof(PinId))]
    public class PinDetailViewModel : BaseViewModel
    {
        private readonly UserRepository userRepository;
        public Command WriteMessageCommand { get; }
        public Command BackCommand { get; }
        public Command DeletePinCommand { get; }
        public ImageSource Img { get; set; }
        public string ShortName { get; set; }
        public int PinId { get; set; }
        public string FullName { get; set; }
        public string Photo { get; set; }
        public string UserUID { get; set; }

        public bool VisibleWriteButton { get; set; }
        public bool VisibleDeleteButton { get; set; }

        private readonly IFirebaseAuthenticator firebaseAuthenticator;

        public PinDetailViewModel()
        {
            firebaseAuthenticator = DependencyService.Get<IFirebaseAuthenticator>();
            userRepository = TinyIoCContainer.Current.Resolve<UserRepository>();

            WriteMessageCommand = new Command(async () => await WriteMessage());
            DeletePinCommand = new Command(async () => await DeletePin());
            BackCommand = new Command(async () => await Back());
            Photo = PhotoBase64DTO.PhotoBase64;

            Img = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(PhotoBase64DTO.PhotoBase64)));
        }

        private async Task Back()
        {
            try
            {
                await Shell.Current.GoToAsync($"..");
            }
            catch(Exception e)
            {
                await Shell.Current.GoToAsync($"//{nameof(MapPage)}");
            }
        }
        
        public void OnAppearing()
        {
            VisibleWriteButton = userRepository.user.UID != UserUID;
            VisibleDeleteButton = !VisibleWriteButton;
        }

        private async Task DeletePin()
        {
            var answer = await MapRequests.DelPin(firebaseAuthenticator, PinId);
            if (answer != "OK")
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Ошибка удаления", "ОК");
            }

            await Shell.Current.GoToAsync("..");
        }

        private async Task WriteMessage()
        {
            var sendText = $"Привет! Меня заинтерисовала твоя метка \"{ShortName}\".";
            await MessageRequests.SendMessage(firebaseAuthenticator, UserUID, sendText);

            await Application.Current.MainPage.DisplayAlert("Уведомление",
                "Сообщение успешно отправлено!\nВы можете написать владельцу в сообщениях или подождать пока, он вам ответит.",
                "ОК");

            await Shell.Current.GoToAsync("..");
        }
    }
}