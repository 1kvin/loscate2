using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Loscate.App.ApiRequests.User;
using Loscate.App.Repository;
using Loscate.App.Services;
using Nancy.TinyIoc;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Loscate.App.ViewModels
{
    public class EditProfileViewModel : BaseViewModel
    {
        public ImageSource UserImage { get; set; }
        public string UserName { get; set; }
        public Command UserImageClickCommand { get; }
        public Command SaveChangesCommand { get; }
        public string base64photo;
        private readonly IFirebaseAuthenticator firebaseAuthenticator;
        private readonly UserRepository userRepository;

        public EditProfileViewModel()
        {
            userRepository = TinyIoCContainer.Current.Resolve<UserRepository>();
            UserName = userRepository.user.Name;
            firebaseAuthenticator = DependencyService.Get<IFirebaseAuthenticator>();
            UserImage = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(userRepository.user.PhotoBase64)));
            UserImageClickCommand = new Command(async () => await UserImageClick());
            SaveChangesCommand = new Command(async () => await SaveChanges());
        }

        public async Task UserImageClick()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Выбор изображения профиля"
            });

            if (result == null) return;

            var stream = await result.OpenReadAsync();

            base64photo = ConvertToBase64(await result.OpenReadAsync());

            if (base64photo.Length / 1024 / 1024 > 20)
            {
                base64photo = null;
                await Application.Current.MainPage.DisplayAlert("Ошибка",
                    "Максимальный размер фото, не должен привышать 20 мб.", "ОК");
                return;
            }

            var resultPhoto = ImageSource.FromStream(() => stream);
            UserImage = resultPhoto;
        }

        public async Task SaveChanges()
        {
            if (!string.IsNullOrEmpty(base64photo))
            {
                var photoAnswer =  await UserRequests.ChangeUserPhoto(firebaseAuthenticator, base64photo);
                
                if (photoAnswer != "OK")
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", "Ошибка изменения фото профиля", "ОК");
                }
                else
                {
                    userRepository.user.PhotoBase64 = base64photo;
                    userRepository.user.UpdateUserImage();
                }
            }

            if (UserName.Length < 10)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Имя пользователя, не может быть меньше 10 символов.", "ОК");
                return;
            }
            
            if (UserName.Length > 30)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Имя пользователя, не может быть больше 30 символов.", "ОК");
                return;
            }

            var nameAnswer = await UserRequests.ChangeUserName(firebaseAuthenticator, UserName);
            if (nameAnswer != "OK")
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Ошибка изменения имени пользователя", "ОК");
            }
            else
            {
                userRepository.user.Name = UserName;
            }
            
           
            
            await Shell.Current.GoToAsync("..");
        }
        
        private string ConvertToBase64(Stream stream)
        {
            var bytes = new Byte[(int)stream.Length];

            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, (int)stream.Length);

            return Convert.ToBase64String(bytes);
        }
    }
}
