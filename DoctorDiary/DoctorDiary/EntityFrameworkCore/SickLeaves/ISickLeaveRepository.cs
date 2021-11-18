using System;
using DoctorDiary.Models.SickLeaves;

namespace DoctorDiary.EntityFrameworkCore.SickLeaves
{
    public interface ISickLeaveRepository : IRepository<SickLeave, Guid>
    {
        
    }
}