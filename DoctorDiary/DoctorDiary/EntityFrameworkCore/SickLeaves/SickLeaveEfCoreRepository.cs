using System;
using DoctorDiary.Models.SickLeaves;

namespace DoctorDiary.EntityFrameworkCore.SickLeaves
{
    public class SickLeaveEfCoreRepository : RepositoryBase<SickLeave, Guid>, ISickLeaveRepository
    {
        public SickLeaveEfCoreRepository() : base()
        {
        }
    }
}