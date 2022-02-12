using System;
using System.Threading.Tasks;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using DoctorDiary.Services.SickLeaves;
using DoctorDiary.Shared.Validations;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.SickLeaves
{
    [QueryProperty(nameof(PatientCardId), nameof(PatientCardId))]
    public class ExtendSickLeaveViewModel : BaseViewModel
    {
        private string _patientCardId;
        private DateTime _lastClosedEndDate;
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
            PropertyChanged += (_, __) => ExtendSickLeaveAsyncCommand.RaiseCanExecuteChanged();
        }

        private async void LoadLastSickLeaveTerm(string patientCardId)
        {
            var sickLeave = await _sickLeaveAppService.GetActiveSickLeaveOrDefaultByPatientCardId(Guid.Parse(patientCardId));

            if (sickLeave == null)
            {
                throw new InvalidCastException("Больничный лист для продления не найден");
            }
            
            _lastClosedEndDate = sickLeave.LastTermEndDate();
            StartDate = _lastClosedEndDate.AddDays(1);
            EndDate = _lastClosedEndDate.AddDays(14);
        }

        private async Task OnExtendSickLeave()
        {
            await _sickLeaveAppService.ExtendSickLeave(
                patientCardId: Guid.Parse(PatientCardId),
                term: Term.Create(startDate: StartDate, endDate: EndDate));
            
            await Shell.Current.GoToAsync("..");
        }
        
        // TODO: Move to validate objects
        private bool ValidateInput(object arg)
        {
            return StartDate > _lastClosedEndDate && EndDate >= StartDate;
        }
    }
}