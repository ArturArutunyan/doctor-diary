using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.EntityFrameworkCore.PatientCards;
using DoctorDiary.EntityFrameworkCore.Visits;
using DoctorDiary.Models.Visits;
using DoctorDiary.Shared.ApplicationContracts;
using Xamarin.Forms;

namespace DoctorDiary.Services.Visits
{
    public class VisitAppService : ApplicationServiceBase, IVisitAppService
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IPatientCardRepository _patientCardRepository;

        public VisitAppService()
        {
            _visitRepository = DependencyService.Get<IVisitRepository>();
            _patientCardRepository = DependencyService.Get<IPatientCardRepository>();
        }
        
        public async Task<List<Visit>> VisitsByDate(DateTime date, bool asNoTracking = false)
        {
            return await _visitRepository.VisitsByDate(date: date, asNoTracking: asNoTracking);
        }

        public async Task Create(Guid patientCardId, DateTime date)
        {
            var patientCard = await _patientCardRepository.GetAsync(patientCardId);
            var visit = new Visit(
                id: Guid.NewGuid(),
                time: date,
                patientCard: patientCard);

            await _visitRepository.InsertAsync(visit);
        }

        public async Task Update(Guid visitId, Guid patientCardId, DateTime time)
        {
            var patientCard = await _patientCardRepository.GetAsync(patientCardId);
            var visit = await _visitRepository.GetAsync(visitId);
            
            visit.ChangePatientCard(patientCard);
            visit.ChangeTime(time);

            await _visitRepository.UpdateAsync(visit);
        }

        public async Task Complete(Guid visitId)
        {
            var visit = await _visitRepository.GetAsync(visitId);
            
            visit.Complete();

            await _visitRepository.UpdateAsync(visit);
        }
    }
}