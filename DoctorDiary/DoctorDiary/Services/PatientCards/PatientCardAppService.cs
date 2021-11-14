using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.EntityFrameworkCore.PatientCards;
using DoctorDiary.Models;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Shared.ApplicationContracts;
using Xamarin.Forms;

namespace DoctorDiary.Services.PatientCards
{
    public class PatientCardAppService : ApplicationServiceBase, IPatientCardAppService
    {
        private readonly IPatientCardRepository _patientCardRepository;

        public PatientCardAppService()
        {
            _patientCardRepository = DependencyService.Get<IPatientCardRepository>();
        }
        
        public async Task<PatientCard> GetAsync(Guid id)
        {
            return await _patientCardRepository.GetAsync(id);
        }

        public async Task<List<PatientCard>> GetListAsync(int count = 5, int skipCount = 0)
        {
            return await _patientCardRepository.GetListAsync(count: count, skipCount: skipCount);
        }

        public async Task CreateAsync(
            string firstName, 
            string lastName, 
            string patronymic, 
            string address, 
            DateTime birthday, 
            string snils,
            string description)
        {
            var patientCard = new PatientCard(
                id: Guid.NewGuid(),
                firstName: firstName,
                lastName: lastName,
                patronymic: patronymic,
                address: address,
                birthday: birthday,
                snils: snils,
                description: description);

            await _patientCardRepository.InsertAsync(patientCard);
        }

        public async Task UpdateAsync(
            Guid id,
            string firstName, 
            string lastName, 
            string patronymic, 
            string address, 
            DateTime birthday, 
            string snils,
            string description)
        {
            var patientCard = await _patientCardRepository.GetAsync(id);
            
            patientCard.ChangeFirstName(firstName);
            patientCard.ChangeLastName(lastName);
            patientCard.ChangePatronymic(patronymic);
            patientCard.ChangeAddress(address);
            patientCard.ChangeBirthday(birthday);
            patientCard.ChangeSnils(snils);
            patientCard.ChangeDescription(description);

            await _patientCardRepository.UpdateAsync(patientCard);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _patientCardRepository.DeleteAsync(id);
        }
    }
}