using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.Models.SickLeaves;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using DoctorDiary.Shared.Application;
using DoctorDiary.Shared.SickLeaves;

namespace DoctorDiary.Services.SickLeaves
{
    public interface ISickLeaveAppService : IApplicationService
    {
        Task<SickLeave> GetAsync(Guid id);

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
        
        Task<SickLeave> CloseSickLeaveWithCodeThirtyOne(SickLeave sickLeave, Term term);
        
        Task DeleteAsync(Guid id);
        
        Task CloseSickLeave(Guid sickLeaveId, SickLeaveCode code, DateTime? startDate = null, DateTime? endDate = null);
    }
}