using System;
using System.Threading.Tasks;

namespace DoctorDiary.Services.MessageBox
{
    public interface IMessageBoxAppService
    {
        void ShowAlert(string title, string message, Action onClosed = null);
        
        Task<string> ShowActionSheet(string title, string cancel, string destruction, string[] buttons = null);
    }
}