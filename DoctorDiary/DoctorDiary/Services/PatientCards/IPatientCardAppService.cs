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

        Task<List<PatientCard>> GetListAsync(int count = 5, int skipCount = 0);

        Task CreateAsync(
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