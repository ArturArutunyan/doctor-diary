using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Models.PatientCards.ValueObjects;
using DoctorDiary.Shared.ApplicationContracts;

namespace DoctorDiary.Services.PatientCards
{
    public interface IPatientCardAppService : IApplicationService
    {
        Task<PatientCard> GetAsync(Guid id);

        Task<List<PatientCard>> PatientCardsByVisits(DateTime date, bool asNoTracking = false);
        
        Task<List<PatientCard>> GetListAsync(int takeCount, int skipCount, bool asNoTracking = false);
        
        Task<List<PatientCard>> GetLastCreatedPatientCards(PatientCardsFilter filter);
        
        Task<PatientCard> CreateAsync(
            string firstName,
            string lastName,
            string patronymic,
            Address address,
            DateTime? birthday,
            Snils snils,
            string description,
            PhoneNumber phoneNumber,
            string gender,
            InsurancePolicy insurancePolicy,
            string placeOfWork,
            string employmentPosition,
            int? precinct);

        Task UpdateAsync(
            Guid id,
            string firstName,
            string lastName,
            string patronymic,
            Address address,
            DateTime? birthday,
            Snils snils,
            string description,
            PhoneNumber phoneNumber,
            string gender,
            InsurancePolicy insurancePolicy,
            string placeOfWork,
            string employmentPosition,
            int? precinct);

        Task DeleteAsync(Guid id);
    }
}