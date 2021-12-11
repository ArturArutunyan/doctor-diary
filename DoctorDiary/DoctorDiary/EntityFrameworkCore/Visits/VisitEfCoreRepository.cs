using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorDiary.Models.Visits;
using Microsoft.EntityFrameworkCore;

namespace DoctorDiary.EntityFrameworkCore.Visits
{
    public class VisitEfCoreRepository : RepositoryBase<Visit, Guid>, IVisitRepository
    {
        public async Task<List<Visit>> VisitsByDate(
            DateTime date,
            bool asNoTracking = false)
        {
            var query = EntityDbSet;

            if (asNoTracking)
            {
                query.AsNoTracking();
            }
            
            return await query.Where(x => x.Time == date).ToListAsync();
        }
    }
}