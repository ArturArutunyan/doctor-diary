using System;
using System.Threading.Tasks;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using DoctorDiary.Services.Reminders;
using DoctorDiary.Services.SickLeaves;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.SickLeaves
{
    [QueryProperty(nameof(PatientCardId), nameof(PatientCardId))]
    public class ExtendSickLeaveViewModel : BaseViewModel
    {
        private string _patientCardId;
        private DateTime _startDate;
        private DateTime _endDate;
        
        private readonly ISickLeaveAppService _sickLeaveAppService;

        public string PatientCardId
        {
            get => _patientCardId;
            set
            {
                _patientCardId = value;
                LoadLastSickLeaveTerm(value);
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }
        
        public AsyncCommand ExtendSickLeaveAsyncCommand { get; }

        public ExtendSickLeaveViewModel()
        {
            _sickLeaveAppService = DependencyService.Get<ISickLeaveAppService>();
            
            ExtendSickLeaveAsyncCommand = new AsyncCommand(OnExtendSickLeave);
        }

        private async void LoadLastSickLeaveTerm(string patientCardId)
        {
            var sickLeave = await _sickLeaveAppService.GetActiveSickLeaveOrDefaultByPatientCardId(Guid.Parse(patientCardId));

            if (sickLeave != null)
            {
                var lastTermEndDate = sickLeave.LastTermEndDate();
                StartDate = lastTermEndDate.AddDays(1);
                EndDate = StartDate.AddDays(14);
            }
        }

        private async Task OnExtendSickLeave()
        {
            await _sickLeaveAppService.ExtendSickLeave(
                patientCardId: Guid.Parse(PatientCardId),
                term: Term.Create(startDate: StartDate, endDate: EndDate));
            
            await Shell.Current.GoToAsync("..");
        }
    }
}