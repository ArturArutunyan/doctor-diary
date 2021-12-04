using System;
using System.Linq;
using System.Threading.Tasks;
using DoctorDiary.Models.SickLeaves;
using Microsoft.EntityFrameworkCore;

namespace DoctorDiary.EntityFrameworkCore.SickLeaves
{
    public class SickLeaveEfCoreRepository : RepositoryBase<SickLeave, Guid>, ISickLeaveRepository
    {
        public async Task<SickLeave> LastOrDefaultSickLeaveForPatientCard(Guid patientCardId)
        {
            return await EntityDbSet.OrderBy(x => x.CreationTime).LastOrDefaultAsync(x => x.PatientCardId == patientCardId);
        }

        public async Task<SickLeave> LastOrDefaultClosedSickLeaveForPatientCard(Guid patientCardId)
        {
            return await EntityDbSet.OrderBy(x => x.CreationTime).LastOrDefaultAsync(x => !x.IsActive && x.PatientCardId == patientCardId);
        }
    }
}