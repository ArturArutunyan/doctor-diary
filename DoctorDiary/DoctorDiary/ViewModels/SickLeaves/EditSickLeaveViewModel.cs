using System;
using System.Linq;
using System.Threading.Tasks;
using DoctorDiary.Services.SickLeaves;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.SickLeaves
{
    [QueryProperty(nameof(PatientCardId), nameof(PatientCardId))]
    public class EditSickLeaveViewModel : BaseViewModel
    {
        private string _patientCardId;
        private Guid _id;
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
                LoadSickLeave();
            }
        }

        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
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

        public AsyncCommand CancelCommand { get; }
        public AsyncCommand EditSickLeaveCommand { get; }


        public EditSickLeaveViewModel()
        {
            _sickLeaveAppService = DependencyService.Get<ISickLeaveAppService>();

            CancelCommand = new AsyncCommand(OnCancel);
            EditSickLeaveCommand = new AsyncCommand(OnEditSickLeave);
            PropertyChanged += (_, __) => EditSickLeaveCommand.RaiseCanExecuteChanged();
        }

        private async Task OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async Task OnEditSickLeave()
        {
            await _sickLeaveAppService.ChangeSickLeave(
                id: Id, 
                number: Number);        
                
            await Shell.Current.GoToAsync("..");
        }

        private async void LoadSickLeave()
        {
            var activeSickLeave = await _sickLeaveAppService.LastOrDefaultSickLeaveForPatientCard(Guid.Parse(PatientCardId));
            var lastClosedSickLeave = await _sickLeaveAppService.LastOrDefaultClosedSickLeaveForPatientCard(Guid.Parse(PatientCardId));

            if (!activeSickLeave.Terms.Any() && lastClosedSickLeave != null)
            {
                _lastClosedEndDate = lastClosedSickLeave.LastTerm().EndDate;
                
                StartDate = lastClosedSickLeave.LastTermEndDate().AddDays(1);
                EndDate = StartDate.AddDays(14);
            }
            else if (activeSickLeave.Terms.Any())
            {
                StartDate = activeSickLeave.LastTermStartDate();
                EndDate = activeSickLeave.LastTermEndDate();
            }
            else
            {
                StartDate = DateTime.Today.Date;
                EndDate = StartDate.AddDays(14);   
            }

            Id = activeSickLeave.Id;
            Number = activeSickLeave.Number;
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