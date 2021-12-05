using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.Models.Reminders;

namespace DoctorDiary.EntityFrameworkCore.Reminders
{
    public interface IReminderRepository : IRepository<Reminder, Guid>
    {
        Task<List<Reminder>> ActiveRemindersForDate(DateTime date, bool asNoTracking = false);
            
        Task<List<Reminder>> GetLastActiveReminders(int take, int skip, bool asNoTracking = false);
    }
}