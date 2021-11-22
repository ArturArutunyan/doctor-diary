using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Shared.Application;

namespace DoctorDiary.Services.PatientCards
{
    public interface IPatientCardAppService : IApplicationService
    {
        Task<PatientCard> GetAsync(Guid id);

        Task<List<PatientCard>> GetListAsync(int takeCount, int skipCount, bool asNoTracking = false);
        
        Task<List<PatientCard>> GetLastCreatedPatientCards(int takeCount, bool asNoTracking);
        
        Task<PatientCard> CreateAsync(
            string firstName,
            string lastName,
            string patronymic,
            string address,
            DateTime birthday,
            string snils,
            string description,
            string phoneNumber);

        Task UpdateAsync(
            Guid id,
            string firstName,
            string lastName,
            string patronymic,
            string address,
            DateTime birthday,
            string snils,
            string description,
            string phoneNumber);

        Task DeleteAsync(Guid id);
    }
}