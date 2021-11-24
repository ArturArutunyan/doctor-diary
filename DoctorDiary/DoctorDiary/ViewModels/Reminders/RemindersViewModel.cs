using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DoctorDiary.Models.Reminders;
using DoctorDiary.Services.Reminders;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.Reminders
{
    public class RemindersViewModel : BaseViewModel
    {
        private const int MaxDefaultRemindersTakeCount = 10;
        private int _remainingItemsThreshold;
        private Reminder _selectedReminder;
        private readonly IReminderAppService _reminderAppService;

        public int RemainingItemsThreshold
        {
            get => _remainingItemsThreshold; 
            set => SetProperty(ref _remainingItemsThreshold, value);
        }
        
        public Reminder SelectedReminder
        {
            get => _selectedReminder;
            set
            {
                SetProperty(ref _selectedReminder, value);
                OnReminderSelected(value);
            }
        }
        
        public ObservableRangeCollection<Reminder> Reminders { get; }
        
        public AsyncCommand LoadRemindersCommand { get; }

        public AsyncCommand<Reminder> ReminderTapped { get; }
        
        public AsyncCommand LoadMoreCommand { get; }
        
        public RemindersViewModel()
        {
            _reminderAppService = DependencyService.Get<IReminderAppService>();
            
            Title = "Напоминания";
            Reminders = new ObservableRangeCollection<Reminder>();
            LoadRemindersCommand = new AsyncCommand(LoadReminders);
            ReminderTapped = new AsyncCommand<Reminder>(OnReminderSelected);
            LoadMoreCommand = new AsyncCommand(OnRemindersThresholdReached);
        }
        
        private async Task OnRemindersThresholdReached()
        {
            if (IsBusy)
                return;
            
            try
            {
                if (RemainingItemsThreshold != -1)
                {
                    var reminders = await _reminderAppService.GetLastActiveReminders(
                        take: MaxDefaultRemindersTakeCount,
                        skip: Reminders.Count,
                        asNoTracking: true);

                    if (reminders.Count != 0)
                    {
                        Reminders.AddRange(reminders);  
                    }
                    else
                    {
                        RemainingItemsThreshold = -1;   
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async Task LoadReminders()
        {
            IsBusy = true;

            try
            {
                Reminders.Clear();
                RemainingItemsThreshold = 3;
                
                var reminders = await _reminderAppService.GetLastActiveReminders(
                    take: MaxDefaultRemindersTakeCount, 
                    skip: 0);
                
                Reminders.AddRange(reminders);
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
            SelectedReminder = null;
        }

        private async Task OnReminderSelected(Reminder reminder)
        {
            await Shell.Current.GoToAsync(reminder.NavigationLinkOnClick);
        }
    }
}