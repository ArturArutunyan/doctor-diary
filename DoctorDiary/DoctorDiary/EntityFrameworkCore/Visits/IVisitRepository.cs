using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.Models.Visits;

namespace DoctorDiary.EntityFrameworkCore.Visits
{
    public interface IVisitRepository : IRepository<Visit, Guid>
    {
        Task<List<Visit>> VisitsByDate(DateTime date, bool asNoTracking = false);
    }
}