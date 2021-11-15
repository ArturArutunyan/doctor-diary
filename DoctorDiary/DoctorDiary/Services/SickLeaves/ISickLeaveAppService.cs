using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.Models;
using DoctorDiary.Models.SickLeaves;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using DoctorDiary.Shared.Application;

namespace DoctorDiary.Services.SickLeaves
{
    public interface ISickLeaveAppService : IApplicationService
    {
        Task<SickLeave> GetAsync(Guid id);

        Task<List<SickLeave>> GetListAsync(
            int count = 5,
            int skipCount = 0);

        Task<List<SickLeave>> GetSickLeavesByPatientCardId(Guid patientCardId);

        Task OpenSickLeaveAsync(
            Guid patientCardId,
            long number,
            Term term);

        Task ExtendSickLeave(Guid id, Term term);

        Task DeleteAsync(Guid id);
    }
}