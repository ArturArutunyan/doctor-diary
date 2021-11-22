using DoctorDiary.Services.Reminders;
using Microsoft.Extensions.DependencyModel;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.Reminders
{
    public class RemindersViewModel : BaseViewModel
    {
        private readonly IReminderAppService _reminderAppService;

        public RemindersViewModel()
        {
            _reminderAppService = DependencyService.Get<IReminderAppService>();
        }
    }
}