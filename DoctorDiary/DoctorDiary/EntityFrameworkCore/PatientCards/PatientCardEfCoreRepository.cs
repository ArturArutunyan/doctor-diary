using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorDiary.Models.PatientCards;
using Microsoft.EntityFrameworkCore;

namespace DoctorDiary.EntityFrameworkCore.PatientCards
{
    public class PatientCardEfCoreRepository : RepositoryBase<PatientCard, Guid>, IPatientCardRepository
    {
        public async Task<List<PatientCard>> GetLastCreatedPatientCards(int takeCount, bool asNoTracking = false)
        {
            var query = EntityDbSet
                .OrderByDescending(x => x.CreationTime)
                .Take(takeCount);

            if (asNoTracking)
            {
                query.AsNoTracking();
            }

            return await query.ToListAsync();
        }
    }
}