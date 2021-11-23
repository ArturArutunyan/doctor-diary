﻿using System;
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
        private Reminder _selectedReminder;
        private readonly IReminderAppService _reminderAppService;

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
        
        public RemindersViewModel()
        {
            _reminderAppService = DependencyService.Get<IReminderAppService>();
            
            Title = "Напоминания";
            Reminders = new ObservableRangeCollection<Reminder>();
            LoadRemindersCommand = new AsyncCommand(LoadReminders);
            ReminderTapped = new AsyncCommand<Reminder>(OnReminderSelected);
        }

        private async Task LoadReminders()
        {
            IsBusy = true;

            try
            {
                Reminders.Clear();
                
                var reminders = await _reminderAppService.GetLastActiveReminders(0, 10);
                
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