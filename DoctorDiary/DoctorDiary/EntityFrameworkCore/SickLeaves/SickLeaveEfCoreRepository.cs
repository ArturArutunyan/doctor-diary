using System;
using System.Linq;
using System.Threading.Tasks;
using DoctorDiary.Models.SickLeaves;
using Microsoft.EntityFrameworkCore;

namespace DoctorDiary.EntityFrameworkCore.SickLeaves
{
    public class SickLeaveEfCoreRepository : RepositoryBase<SickLeave, Guid>, ISickLeaveRepository
    {
        public async Task<SickLeave> LastSickLeaveForPatientCard(Guid patientCardId)
        {
            return await EntityDbSet.OrderBy(x => x.CreationTime).LastOrDefaultAsync();
        }
    }
}