using System;
using System.Threading.Tasks;
using DoctorDiary.Shared.ApplicationContracts;
using Xamarin.Forms;

namespace DoctorDiary.Services.MessageBox
{
    public class MessageBoxAppService : ApplicationServiceBase, IMessageBoxAppService
    {
        public async void ShowAlert(string title, string message, Action onClosed = null)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "ОК");
            onClosed?.Invoke();
        }

        public async Task<string> ShowActionSheet(string title, string cancel, string destruction = null, string[] buttons = null)
        {
            var displayButtons = buttons ?? new string[] { };
            var action = await Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, displayButtons);
            return action;
        }
    }
}