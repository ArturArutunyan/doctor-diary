using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.EntityFrameworkCore.PatientCards;
using DoctorDiary.EntityFrameworkCore.SickLeaves;
using DoctorDiary.Models.SickLeaves;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using DoctorDiary.Shared.ApplicationContracts;
using Xamarin.Forms;

namespace DoctorDiary.Services.SickLeaves
{
    public class SickLeaveAppService : ApplicationServiceBase, ISickLeaveAppService
    {
        private readonly ISickLeaveRepository _sickLeaveRepository;
        private readonly IPatientCardRepository _patientCardRepository;

        public SickLeaveAppService()
        {
            _sickLeaveRepository = DependencyService.Get<ISickLeaveRepository>();
            _patientCardRepository = DependencyService.Get<IPatientCardRepository>();
        }
        
        public async Task<SickLeave> GetAsync(Guid id)
        {
            return await _sickLeaveRepository.GetAsync(id);
        }

        public async Task<List<SickLeave>> GetListAsync(
            int count = 5,
            int skipCount = 0)
        {
            return await _sickLeaveRepository.GetListAsync(count, skipCount);
        }

        public async Task<List<SickLeave>> GetSickLeavesByPatientCardId(Guid patientCardId)
        {
            return await _sickLeaveRepository.GetListAsync(x => x.PatientCardId == patientCardId);
        }

        public async Task OpenSickLeaveAsync(Guid patientCardId, Term term)
        {
            var patientCard = await _patientCardRepository.GetAsync(patientCardId);
            var sickLeave = new SickLeave(
                id: Guid.NewGuid(),
                number: number,
                patientCard: patientCard,
                term: term);

            await _sickLeaveRepository.InsertAsync(sickLeave);
        }

        public async Task ExtendSickLeave(Guid id, Term term)
        {
            var sickLeave = await _sickLeaveRepository.GetAsync(id);
            
            sickLeave.ExtendSickLeave(term: term);

            await _sickLeaveRepository.UpdateAsync(sickLeave);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _sickLeaveRepository.DeleteAsync(id);
        }
    }
}