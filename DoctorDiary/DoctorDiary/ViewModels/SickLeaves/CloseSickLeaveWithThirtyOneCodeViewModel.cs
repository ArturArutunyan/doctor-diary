using System;
using System.Threading.Tasks;
using DoctorDiary.Services.SickLeaves;
using DoctorDiary.Shared.SickLeaves;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.SickLeaves
{
    [QueryProperty(nameof(SickLeaveId), nameof(SickLeaveId))]
    public class CloseSickLeaveWithThirtyOneCodeViewModel : BaseViewModel
    {
        private string _sickLeaveId;
        private long? _number;
        private DateTime _startDate;
        private DateTime _endDate;
        private DateTime _lastClosedEndDate;
        private readonly ISickLeaveAppService _sickLeaveAppService;

        public string SickLeaveId
        {
            get => _sickLeaveId;
            set
            {
                _sickLeaveId = value;  
                InitPropertiesToDefaultValues();
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

        public long? Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }

        public AsyncCommand CancelCommand { get; }
        public AsyncCommand CloseSickLeaveWithCodeCommand { get; }


        public CloseSickLeaveWithThirtyOneCodeViewModel()
        {
            _sickLeaveAppService = DependencyService.Get<ISickLeaveAppService>();
            
            CancelCommand = new AsyncCommand(OnCancel);
            CloseSickLeaveWithCodeCommand = new AsyncCommand(OnCloseSickLeaveWithCode);
            PropertyChanged += (_, __) => CloseSickLeaveWithCodeCommand.RaiseCanExecuteChanged();
        }

        private async Task OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void InitPropertiesToDefaultValues()
        {
            var lastSickLeave = await _sickLeaveAppService.GetAsync(Guid.Parse(SickLeaveId));
            
            _lastClosedEndDate = lastSickLeave.LastTermEndDate();

            StartDate = _lastClosedEndDate.AddDays(1);
            EndDate = StartDate.AddDays(14);
        }

        private async Task OnCloseSickLeaveWithCode()
        {
            try
            {
                await _sickLeaveAppService.CloseSickLeaveWithCode(
                    sickLeaveId: Guid.Parse(SickLeaveId),
                    code: SickLeaveCode.ThirtyOne,
                    number: Number,
                    startDate: StartDate,
                    endDate: EndDate);
                
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        private bool ValidateInput(object arg)
        {
            return StartDate > _lastClosedEndDate && EndDate >= StartDate;
        }
    }
}