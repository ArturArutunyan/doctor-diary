using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DoctorDiary.Models.SickLeaves;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using DoctorDiary.Shared.ApplicationContracts;
using DoctorDiary.Shared.SickLeaves;

namespace DoctorDiary.Services.SickLeaves
{
    public interface ISickLeaveAppService : IApplicationService
    {
        Task<SickLeave> GetAsync(Guid id);

        Task<SickLeave> LastOrDefaultSickLeaveForPatientCard(Guid patientCardId);
        
        Task<SickLeave> LastOrDefaultClosedSickLeaveForPatientCard(Guid patientCardId);

        Task<List<SickLeave>> GetListAsync(
            int count = 5,
            int skipCount = 0);

        Task<List<SickLeave>> GetSickLeavesByPatientCardId(Guid patientCardId);
        
        Task<SickLeave> GetActiveSickLeaveOrDefaultByPatientCardId(Guid patientCardId);

        Task OpenSickLeave(
            Guid patientCardId,
            long number,
            Term term);

        Task ExtendSickLeave(
            Guid patientCardId,
            Term term);

        Task CloseSickLeave(Guid sickLeaveId);
        
        Task CloseSickLeaveWithCode(
            Guid sickLeaveId, 
            SickLeaveCode code, 
            long? number, 
            DateTime? startDate,
            DateTime? endDate);
        
        Task DeleteAsync(Guid id);

        Task ChangeSickLeave(Guid id, long number);
    }
}