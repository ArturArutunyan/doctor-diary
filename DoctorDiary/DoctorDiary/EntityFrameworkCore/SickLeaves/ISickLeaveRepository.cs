using System;
using System.Threading.Tasks;
using DoctorDiary.Models.SickLeaves;

namespace DoctorDiary.EntityFrameworkCore.SickLeaves
{
    public interface ISickLeaveRepository : IRepository<SickLeave, Guid>
    {
        Task<SickLeave> LastSickLeaveForPatientCard(Guid patientCardId);
    }
}