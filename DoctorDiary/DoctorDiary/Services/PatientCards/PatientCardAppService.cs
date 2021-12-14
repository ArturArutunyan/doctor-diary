﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorDiary.EntityFrameworkCore.PatientCards;
using DoctorDiary.EntityFrameworkCore.Visits;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Models.PatientCards.ValueObjects;
using DoctorDiary.Shared.ApplicationContracts;
using Xamarin.Forms;

namespace DoctorDiary.Services.PatientCards
{
    public class PatientCardAppService : ApplicationServiceBase, IPatientCardAppService
    {
        private readonly IPatientCardRepository _patientCardRepository;
        private readonly IVisitRepository _visitRepository;

        public PatientCardAppService()
        {
            _patientCardRepository = DependencyService.Get<IPatientCardRepository>();
            _visitRepository = DependencyService.Get<IVisitRepository>();
        }
        
        public async Task<PatientCard> GetAsync(Guid id)
        {
            return await _patientCardRepository.GetAsync(id);
        }

        public async Task<List<PatientCard>> PatientCardsByVisits(DateTime date, bool asNoTracking = false)
        {
            var visits = await _visitRepository.GetListAsync(x => x.Time == date);
            var patientCardIds = visits.Select(x => x.PatientCardId);

            return await _patientCardRepository.GetListAsync(x => patientCardIds.Contains(x.Id), asNoTracking: true);
        }

        public async Task<List<PatientCard>> GetListAsync(int takeCount, int skipCount, bool asNoTracking = false)
        {
            return await _patientCardRepository.GetListAsync(takeCount: takeCount, skipCount: skipCount, asNoTracking: asNoTracking);
        }

        public async Task<List<PatientCard>> GetLastCreatedPatientCards(int takeCount, int skipCount, bool asNoTracking = false)
        {
            return await _patientCardRepository.GetLastCreatedPatientCards(
                takeCount: takeCount, 
                skipCount: skipCount,
                asNoTracking: asNoTracking);
        }

        public async Task<PatientCard> CreateAsync(
            string firstName, 
            string lastName, 
            string patronymic, 
            Address address, 
            DateTime birthday, 
            Snils snils,
            string description,
            PhoneNumber phoneNumber,
            string gender,
            InsurancePolicy insurancePolicy,
            string placeOfWork,
            int precinct)
        {
            var patientCard = new PatientCard(
                id: Guid.NewGuid(),
                firstName: firstName,
                lastName: lastName,
                patronymic: patronymic,
                address: address,
                birthday: birthday,
                snils: snils,
                description: description,
                phoneNumber: phoneNumber,
                gender: gender,
                insurancePolicy: insurancePolicy,
                placeOfWork: placeOfWork,
                precinct: precinct);
            
            await _patientCardRepository.InsertAsync(patientCard);
            
            return patientCard;
        }

        public async Task UpdateAsync(
            Guid id,
            string firstName, 
            string lastName, 
            string patronymic, 
            Address address, 
            DateTime birthday, 
            Snils snils,
            string description,
            PhoneNumber phoneNumber,
            string gender,
            InsurancePolicy insurancePolicy,
            string placeOfWork,
            int precinct)
        {
            var patientCard = await _patientCardRepository.GetAsync(id);
            
            patientCard.ChangeFirstName(firstName);
            patientCard.ChangeLastName(lastName);
            patientCard.ChangePatronymic(patronymic);
            patientCard.ChangeAddress(address);
            patientCard.ChangeBirthday(birthday);
            patientCard.ChangeSnils(snils);
            patientCard.ChangeDescription(description);
            patientCard.ChangePhoneNumber(phoneNumber);
            patientCard.ChangeGender(gender);
            patientCard.ChangeInsurancePolicy(insurancePolicy);
            patientCard.ChangePlaceOfWork(placeOfWork);
            patientCard.ChangePrecinct(precinct);

            await _patientCardRepository.UpdateAsync(patientCard);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _patientCardRepository.DeleteAsync(id);
        }
    }
}