using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.Models.Visits;
using DoctorDiary.Shared.ApplicationContracts;

namespace DoctorDiary.Services.Visits
{
    public interface IVisitAppService : IApplicationService
    {
        Task<List<Visit>> VisitsByDate(
            DateTime date,
            bool asNoTracking = false);

        Task Create(Guid patientCardId, DateTime date);

        Task Update(Guid visitId, Guid patientCardId, DateTime time);

        Task Complete(Guid visitId);
    }
}