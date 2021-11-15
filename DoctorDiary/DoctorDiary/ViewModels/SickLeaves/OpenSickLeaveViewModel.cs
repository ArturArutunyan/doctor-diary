using System;
using System.Threading.Tasks;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using DoctorDiary.Services.SickLeaves;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.SickLeaves
{
    [QueryProperty(nameof(PatientCardId), nameof(PatientCardId))]
    public class OpenSickLeaveViewModel : BaseViewModel
    {
        private string _patientCardId;
        private long _number;
        private DateTime _startDate;
        private DateTime _endDate;
        
        private readonly ISickLeaveAppService _sickLeaveAppService;

        public OpenSickLeaveViewModel()
        {
            _sickLeaveAppService = DependencyService.Get<ISickLeaveAppService>();

            OpenSickLeaveAsyncCommand = new AsyncCommand(OnOpenSickLeave);
        }

        public string PatientCardId
        {
            get => _patientCardId;
            set => _patientCardId = value;
        }

        public long Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
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

        private async Task OnOpenSickLeave()
        {
            await _sickLeaveAppService.OpenSickLeaveAsync(
                patientCardId: Guid.Parse(PatientCardId), 
                number: Number,
                term: Term.Create(startDate: StartDate, endDate: EndDate));
            
            await Shell.Current.GoToAsync("..");
        }

        public AsyncCommand OpenSickLeaveAsyncCommand { get; }
    }
}