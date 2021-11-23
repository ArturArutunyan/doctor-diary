using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.Models.Reminders;

namespace DoctorDiary.EntityFrameworkCore.Reminders
{
    public interface IReminderRepository : IRepository<Reminder, Guid>
    {
        Task<List<Reminder>> GetLastActiveReminders(int page, int pageSize, bool asNoTracking = true);
    }
}