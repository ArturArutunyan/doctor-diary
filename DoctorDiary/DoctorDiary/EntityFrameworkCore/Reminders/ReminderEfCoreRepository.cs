using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorDiary.Models.Reminders;
using Microsoft.EntityFrameworkCore;

namespace DoctorDiary.EntityFrameworkCore.Reminders
{
    public class ReminderEfCoreRepository : RepositoryBase<Reminder, Guid>, IReminderRepository
    {
        public async Task<List<Reminder>> GetActiveReminders(int page, int pageSize, bool asNoTracking = true)
        {
            var query = EntityDbSet.Skip(page * pageSize).Take(pageSize);

            if (asNoTracking)
            {
                query.AsNoTracking();
            }

            return await query.Where(x => !x.IsClosed)
                .OrderByDescending(x => x.Time)
                .ToListAsync();
        }
    }
}