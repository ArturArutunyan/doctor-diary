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
        public async Task<List<Reminder>> ActiveRemindersForDate(DateTime date, bool asNoTracking = false)
        {
            var query = EntityDbSet;

            if (asNoTracking)
            {
                query.AsNoTracking();
            }

            return await query.Where(x => !x.IsClosed && x.Time == date)
                .OrderByDescending(x => x.Title)
                .ToListAsync();
        }

        public async Task<List<Reminder>> GetLastActiveReminders(int take, int skip, bool asNoTracking = false)
        {
            var query = EntityDbSet;

            if (asNoTracking)
            {
                query.AsNoTracking();
            }

            return await query.Where(x => !x.IsClosed)
                .OrderByDescending(x => x.Time)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }
    }
}