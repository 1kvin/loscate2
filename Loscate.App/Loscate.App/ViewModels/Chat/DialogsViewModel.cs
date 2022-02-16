using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Loscate.App.ApiRequests.Social.Dialog;
using Loscate.App.ApiRequests.Social.Message;
using Loscate.App.Models;
using Loscate.App.Services;
using Loscate.App.Views;
using Loscate.DTO.Firebase;
using Loscate.DTO.Social;
using Xamarin.Forms;

namespace Loscate.App.ViewModels
{
    public class DialogsViewModel : BaseViewModel
    {
        private readonly IFirebaseAuthenticator firebaseAuthenticator;
        private readonly INotificationManager notificationManager;
        private XamarinDialog _selectedDialog;
        public ObservableCollection<XamarinDialog> Dialogs { get; }
        public Command LoadDialogsCommand { get; }
        public Command AddDialogCommand { get; }
        public Command<XamarinDialog> ItemTapped { get; }
        
        public XamarinDialog SelectedDialog
        {
            get => _selectedDialog;
            set
            {
                SetProperty(ref _selectedDialog, value);
                OnDialogSelected(value);
            }
        }
        
        public DialogsViewModel()
        {
            Dialogs = new ObservableCollection<XamarinDialog>();
            firebaseAuthenticator = DependencyService.Get<IFirebaseAuthenticator>();
            notificationManager = DependencyService.Get<INotificationManager>();
            LoadDialogsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<XamarinDialog>(OnDialogSelected);

            AddDialogCommand = new Command(OnAddDialog);
            
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
            
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                Task.Run(async () => await CheckNewMessage());
                return true;
            });
        }
        
        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
            });
        }

        async Task CheckNewMessage()
        {
            var dialogs = await DialogRequests.GetUserDialogs(firebaseAuthenticator);
            foreach (var dialog in dialogs)
            {
                var d = Dialogs.SingleOrDefault(c=>c.XamarinCompanion.Name == dialog.Companion.Name);
                if (d == null)
                {
                    if (dialog.LastMessage.Text.Contains("Привет! Меня заинтерисовала твоя метка"))
                    {
                        notificationManager.SendNotification("Новый отклик", $"На вашу метку ответил пользователь {dialog.Companion.Name}.");
                    }

                    await ExecuteLoadItemsCommand();
                    return;
                }
                else
                {
                    if (d.LastMessage.Text != dialog.LastMessage.Text)
                    {
                        if (dialog.LastMessage.Text.Contains("Привет! Меня заинтерисовала твоя метка"))
                        {
                            notificationManager.SendNotification("Новый отклик", $"На вашу метку ответил пользователь {dialog.Companion.Name}.");
                        }
                        
                        await ExecuteLoadItemsCommand();
                        return;
                    }
                }
            }
        }
        
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                var dialogs = await DialogRequests.GetUserDialogs(firebaseAuthenticator);
                
                // foreach (var dialog in dialogs)
                // {
                //     var d = Dialogs.SingleOrDefault(d=>d.XamarinCompanion.Name == dialog.Companion.Name);
                //     if (d == null)
                //     {
                //         if (dialog.LastMessage.Text.Contains("Привет! Меня заинтерисовала твоя метка"))
                //         {
                //             notificationManager.SendNotification("Новый отклик", $"На вашу метку ответил пользователь {dialog.Companion.Name}.");
                //         }
                //         Dialogs.Add(new XamarinDialog(dialog));
                //     }
                //     else
                //     {
                //
                //         d.LastMessage = dialog.LastMessage;
                //     }
                // }
                
                Dialogs.Clear();
                foreach (var dialog in dialogs)
                {
                    Dialogs.Add(new XamarinDialog(dialog));
                }


                    // var items = await DataStore.GetItemsAsync(true);
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
            SelectedDialog = null;
        }
        
        private async void OnAddDialog(object obj)
        {
            //await Shell.Current.GoToAsync(nameof(NewItemPage));
        }
        
        async void OnDialogSelected(Dialog dialog)
        {
            if (dialog == null)
                return;
            
           await Shell.Current.GoToAsync($"{nameof(ChatPage)}?{nameof(ChatPageViewModel.CompanionUserUID)}={dialog.Companion.UID}&{nameof(ChatPageViewModel.CompanionName)}={dialog.Companion.Name}");
        }
    }
}