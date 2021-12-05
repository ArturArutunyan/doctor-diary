using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorDiary.EntityFrameworkCore.PatientCards;
using DoctorDiary.EntityFrameworkCore.SickLeaves;
using DoctorDiary.Models.PatientCards;
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

        public async Task<SickLeave> LastOrDefaultSickLeaveForPatientCard(Guid patientCardId)
        {
            return await _sickLeaveRepository.LastOrDefaultSickLeaveForPatientCard(patientCardId);
        }

        public async Task<SickLeave> LastOrDefaultClosedSickLeaveForPatientCard(Guid patientCardId)
        {
            return await _sickLeaveRepository.LastOrDefaultClosedSickLeaveForPatientCard(patientCardId);
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
            var sickLeave = new SickLeave(
                id: Guid.NewGuid(),
                number: number,
                patientCardId: patientCardId,
                term: term);

            await PushReminder(patientCardId: patientCardId, time: term.EndDate);

            await _sickLeaveRepository.InsertAsync(sickLeave);
        }

        public async Task ExtendSickLeave(Guid patientCardId, Term term)
        {
            var sickLeave = await GetActiveSickLeaveOrDefaultByPatientCardId(patientCardId);
            sickLeave.ExtendSickLeave(term);

            await PushReminder(patientCardId: patientCardId, time: term.EndDate);
            
            await _sickLeaveRepository.UpdateAsync(sickLeave);
        }

        public async Task CloseSickLeave(Guid sickLeaveId)
        {
            var sickLeave = await _sickLeaveRepository.GetAsync(sickLeaveId);
            
            sickLeave.Close();

            await _sickLeaveRepository.UpdateAsync(sickLeave);
        }

        public async Task CloseSickLeaveWithCode(
            Guid sickLeaveId, 
            SickLeaveCode code, 
            long? number, 
            DateTime? startDate,
            DateTime? endDate)
        {
            switch (code)
            {
                case SickLeaveCode.ThirtyOne:
                    await CloseSickLeaveWithCodeThirtyOne(
                        sickLeaveId: sickLeaveId, 
                        number: number, 
                        startDate: startDate, 
                        endDate: endDate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(code), code, "Неизвестный код больничного листа");
            }
        }

        public async Task CloseSickLeaveWithCodeThirtyOne(
            Guid sickLeaveId, 
            long? number,
            DateTime? startDate,
            DateTime? endDate)
        {
            var sickLeave = await _sickLeaveRepository.GetAsync(sickLeaveId);
            
            sickLeave.Close();
            
            await _sickLeaveRepository.UpdateAsync(sickLeave);

            // TODO: Validate - startDate & endDate of new sick leave should be greater than previous
            if (number.HasValue && startDate.HasValue && endDate.HasValue)
            {
                await OpenSickLeave(
                    patientCardId: sickLeave.PatientCardId,
                    number: number.Value,
                    term: Term.Create(startDate: startDate.Value, endDate: endDate.Value));
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            await _sickLeaveRepository.DeleteAsync(id);
        }

        public async Task ChangeSickLeave(Guid id, long number, IEnumerable<Term> terms)
        {
            var sickLeave = await _sickLeaveRepository.GetAsync(id);

            sickLeave.ChangeNumber(number: number);
            sickLeave.ChangeTerms(terms: terms);

            await _sickLeaveRepository.UpdateAsync(sickLeave);
        }

        private async Task PushReminder(Guid patientCardId, DateTime time)
        {
            var patientCard = await _patientCardRepository.GetAsync(patientCardId);
            
            var fullName = patientCard.LastName + " " + string.Join('.', new[]
                {
                    patientCard.FirstName,
                    patientCard.Patronymic
                }
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x[0]));
            
            await _reminderAppService.Push(
                title: $@"Заканчивается больничный",
                description: $"У пациента {fullName} сегодня заканчивается больничный лист",
                navigationLinkOnClick: $"{nameof(PatientCardDetailPage)}?{nameof(PatientCardDetailViewModel.PatientCardId)}={patientCard.Id}",
                time: time);
        }
    }
}