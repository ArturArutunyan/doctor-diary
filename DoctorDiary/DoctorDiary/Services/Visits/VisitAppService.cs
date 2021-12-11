using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.EntityFrameworkCore.Visits;
using DoctorDiary.Models.Visits;
using DoctorDiary.Shared.ApplicationContracts;
using Xamarin.Forms;

namespace DoctorDiary.Services.Visits
{
    public class VisitAppService : ApplicationServiceBase, IVisitAppService
    {
        private readonly IVisitRepository _visitRepository;

        public VisitAppService()
        {
            _visitRepository = DependencyService.Get<IVisitRepository>();
        }
        
        public async Task<List<Visit>> VisitsByDate(DateTime date, bool asNoTracking = false)
        {
            return await _visitRepository.VisitsByDate(date: date, asNoTracking: asNoTracking);
        }
    }
}