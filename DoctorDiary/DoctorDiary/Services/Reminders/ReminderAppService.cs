using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.EntityFrameworkCore.Reminders;
using DoctorDiary.Models.Reminders;
using DoctorDiary.Shared.ApplicationContracts;
using Xamarin.Forms;

namespace DoctorDiary.Services.Reminders
{
    public class ReminderAppService : ApplicationServiceBase, IReminderAppService
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderAppService()
        {
            _reminderRepository = DependencyService.Get<IReminderRepository>();
        }

        public async Task<List<Reminder>> ActiveRemindersForDate(DateTime date, bool asNoTracking = false)
        {
            return await _reminderRepository.ActiveRemindersForDate(date: date, asNoTracking: asNoTracking);
        }

        public async Task<List<Reminder>> GetLastActiveReminders(int take, int skip, bool asNoTracking = false)
        {
            return await _reminderRepository.GetLastActiveReminders(take, skip);
        }

        public async Task Push(
            string title,
            string description,
            string navigationLinkOnClick,
            DateTime? time = null)
        {
            var reminder = new Reminder(
                id: Guid.NewGuid(),
                title: title,
                description: description,
                time: time,
                navigationLinkOnClick: navigationLinkOnClick);

            await _reminderRepository.InsertAsync(reminder);
        }

        public async Task Close(Guid reminderId)
        {
            var reminder = await _reminderRepository.GetAsync(reminderId);
            
            reminder.Close();

            await _reminderRepository.UpdateAsync(reminder);
        }
        
        public async Task Disable(Guid reminderId)
        {
            var reminder = await _reminderRepository.GetAsync(reminderId);
            
            reminder.Disable();

            await _reminderRepository.UpdateAsync(reminder);
        }
    }
}