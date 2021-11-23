using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorDiary.EntityFrameworkCore.PatientCards;
using DoctorDiary.EntityFrameworkCore.SickLeaves;
using DoctorDiary.Models.SickLeaves;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using DoctorDiary.Services.Reminders;
using DoctorDiary.Shared.ApplicationContracts;
using DoctorDiary.Shared.SickLeaves;
using DoctorDiary.ViewModels.PatientCards;
using DoctorDiary.Views.PatientCards;
using Xamarin.Forms;

namespace DoctorDiary.Services.SickLeaves
{
    public class SickLeaveAppService : ApplicationServiceBase, ISickLeaveAppService
    {
        private readonly ISickLeaveRepository _sickLeaveRepository;
        private readonly IPatientCardRepository _patientCardRepository;
        private readonly IReminderAppService _reminderAppService;

        public SickLeaveAppService()
        {
            _sickLeaveRepository = DependencyService.Get<ISickLeaveRepository>();
            _patientCardRepository = DependencyService.Get<IPatientCardRepository>();
            _reminderAppService = DependencyService.Get<IReminderAppService>();
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
            var sickLeaves = await _sickLeaveRepository.GetListAsync(x => x.PatientCardId == patientCardId);
            return sickLeaves.OrderByDescending(t => t.LastTermEndDate()).ToList();
        }

        public async Task<SickLeave> GetActiveSickLeaveOrDefaultByPatientCardId(Guid patientCardId)
        {
            return await _sickLeaveRepository.FindAsync(x => x.PatientCardId == patientCardId && x.IsActive);
        }

        public async Task OpenSickLeave(Guid patientCardId, long number, Term term)
        {
            var patientCard = await _patientCardRepository.GetAsync(patientCardId);
            var sickLeave = new SickLeave(
                id: Guid.NewGuid(),
                number: number,
                patientCard: patientCard,
                term: term);

            var fullName = patientCard.LastName + " " + string.Join('.', new[]
                {
                    patientCard.FirstName,
                    patientCard.Patronymic
                }
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x[0]));
            
            await _reminderAppService.Create(
                title: $@"У {fullName} сегодня заканчивается больничный лист",
                description: "Нажмите на это напоминание, чтобы перейти в карточку пациента",
                navigationLinkOnClick: $"{nameof(PatientCardDetailPage)}?{nameof(PatientCardDetailViewModel.PatientCardId)}={patientCard.Id}",
                time: term.EndDate);
                
            await _sickLeaveRepository.InsertAsync(sickLeave);
        }

        public async Task ExtendSickLeave(Guid patientCardId, Term term)
        {
            var sickLeave = await GetActiveSickLeaveOrDefaultByPatientCardId(patientCardId);
            
            sickLeave.ExtendSickLeave(term);

            await _sickLeaveRepository.UpdateAsync(sickLeave);
        }

        public async Task<SickLeave> CloseSickLeave(SickLeave sickLeave)
        {
            sickLeave.Close();

            return await _sickLeaveRepository.UpdateAsync(sickLeave);
        }

        public async Task CloseSickLeave(Guid sickLeaveId)
        {
            var sickLeave = await _sickLeaveRepository.GetAsync(sickLeaveId);
            
            sickLeave.Close();

            await _sickLeaveRepository.UpdateAsync(sickLeave);
        }

        public async Task<SickLeave> CloseSickLeaveWithCodeThirtyOne(SickLeave sickLeave, Term term)
        {
            sickLeave.ExtendSickLeave(term);

            return await _sickLeaveRepository.UpdateAsync(sickLeave);
        }
        
        public async Task CloseSickLeaveWithCodeThirtyOne(Guid sickLeaveId, Term term)
        {
            var sickLeave = await _sickLeaveRepository.GetAsync(sickLeaveId);
            
            sickLeave.ExtendSickLeave(term);

            await _sickLeaveRepository.UpdateAsync(sickLeave);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _sickLeaveRepository.DeleteAsync(id);
        }

        public async Task CloseSickLeave(Guid sickLeaveId, SickLeaveCode code, DateTime? startDate = null, DateTime? endDate = null)
        {
            switch (code)
            {
                case SickLeaveCode.Empty:
                    await CloseSickLeave(sickLeaveId);
                    break;
                case SickLeaveCode.ThirtyOne:
                    if (startDate.HasValue && endDate.HasValue)
                    {
                        await CloseSickLeaveWithCodeThirtyOne(
                            sickLeaveId: sickLeaveId, 
                            term: Term.Create(startDate: startDate.Value, endDate: endDate.Value));    
                    }
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}