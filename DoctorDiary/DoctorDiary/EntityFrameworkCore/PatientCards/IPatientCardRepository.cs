using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.Models.PatientCards;

namespace DoctorDiary.EntityFrameworkCore.PatientCards
{
    public interface IPatientCardRepository : IRepository<PatientCard, Guid>
    {
        Task<List<PatientCard>> PatientCardsByVisits(DateTime date, bool asNoTracking = false);
        
        Task<List<PatientCard>> GetLastCreatedPatientCards(int takeCount,
            int skipCount,
            bool asNoTracking = false,
            string firstName = null,
            string lastName = null,
            string patronymic = null,
            string city = null,
            string street = null,
            string apartment = null,
            string house = null,
            DateTime? birthday = null,
            int? yearOfBirth = null,
            string snils = null,
            string description = null,
            string phoneNumber = null,
            string gender = null,
            string insurancePolicy = null,
            string placeOfWork = null,
            int? precinct = null);

        Task<List<PatientCard>> GetListByFilterAsync(int takeCount, int skipCount, bool asNoTracking = false);
    }
}