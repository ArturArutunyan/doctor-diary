using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.Models.Visits;
using DoctorDiary.Shared.Application;

namespace DoctorDiary.Services.Visits
{
    public interface IVisitAppService : IApplicationService
    {
        Task<List<Visit>> VisitsByDate(
            DateTime date,
            bool asNoTracking = false);
    }
}