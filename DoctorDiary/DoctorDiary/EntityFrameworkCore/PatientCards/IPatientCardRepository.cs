using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.Models.PatientCards;

namespace DoctorDiary.EntityFrameworkCore.PatientCards
{
    public interface IPatientCardRepository : IRepository<PatientCard, Guid>
    {
        Task<List<PatientCard>> PatientCardsByVisits(DateTime date, bool asNoTracking = false);
        
        Task<List<PatientCard>> GetLastCreatedPatientCards(int takeCount, int skipCount, bool asNoTracking = false);
    }
}