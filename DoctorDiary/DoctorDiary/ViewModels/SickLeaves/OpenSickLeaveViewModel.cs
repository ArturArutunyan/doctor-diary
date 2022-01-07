using System;
using System.Threading.Tasks;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using DoctorDiary.Services.SickLeaves;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.SickLeaves
{
    [QueryProperty(nameof(PatientCardId), nameof(PatientCardId))]
    public class OpenSickLeaveViewModel : BaseViewModel
    {
        private string _patientCardId;
        private long? _number;
        private DateTime _startDate;
        private DateTime _endDate;
        private DateTime? _lastClosedEndDate;

        private readonly ISickLeaveAppService _sickLeaveAppService;

        public string PatientCardId
        {
            get => _patientCardId;
            set
            {
                _patientCardId = value; 
                InitPropertiesToDefaultValues();
            }
        }

        public long? Number
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

        public AsyncCommand OpenSickLeaveAsyncCommand { get; }

        public OpenSickLeaveViewModel()
        {
            _sickLeaveAppService = DependencyService.Get<ISickLeaveAppService>();

            OpenSickLeaveAsyncCommand = new AsyncCommand(OnOpenSickLeave, ValidateInput);
            PropertyChanged += (_, __) => OpenSickLeaveAsyncCommand.RaiseCanExecuteChanged();
        }

        private async Task OnOpenSickLeave()
        {
            await _sickLeaveAppService.OpenSickLeave(
                patientCardId: Guid.Parse(PatientCardId), 
                number: Number,
                term: Term.Create(startDate: StartDate, endDate: EndDate));        
                
            await Shell.Current.GoToAsync("..");
        }

        private async void InitPropertiesToDefaultValues()
        {
            var lastSickLeave = await _sickLeaveAppService.LastOrDefaultSickLeaveForPatientCard(Guid.Parse(PatientCardId));

            if (lastSickLeave != null)
            {
                if (lastSickLeave.IsActive)
                {
                    // TODO: add custom exception
                    throw new InvalidOperationException("Невозможно открыть больничный лист, так как уже существует другой открытый больничный лист");
                }
                
                _lastClosedEndDate = lastSickLeave.LastTerm().EndDate;
                
                StartDate = lastSickLeave.LastTermEndDate().AddDays(1);
                EndDate = StartDate.AddDays(14);
            }
            else
            {
                StartDate = DateTime.Today.Date;
                EndDate = StartDate.AddDays(14);   
            }
        }

        private bool ValidateInput(object arg)
        {
            if (_lastClosedEndDate.HasValue)
            {
                return StartDate > _lastClosedEndDate && EndDate >= StartDate;
            }

            return true;
        }
    }
}