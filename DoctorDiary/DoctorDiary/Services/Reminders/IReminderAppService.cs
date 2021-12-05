using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.Models.Reminders;

namespace DoctorDiary.Services.Reminders
{
    public interface IReminderAppService
    {
        Task<List<Reminder>> ActiveRemindersForDate(DateTime date, bool asNoTracking = false);
        
        Task<List<Reminder>> GetLastActiveReminders(int take, int skip, bool asNoTracking = false);

        Task Create(
            string title,
            string description,
            string navigationLinkOnClick,
            DateTime? time = null);

        Task Close(Guid reminderId);
        
        Task Disable(Guid reminderId);
    }
}