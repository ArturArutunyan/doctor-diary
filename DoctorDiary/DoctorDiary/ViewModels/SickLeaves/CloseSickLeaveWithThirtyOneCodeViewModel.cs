using System;
using System.Threading.Tasks;
using DoctorDiary.Services.SickLeaves;
using DoctorDiary.Shared.SickLeaves;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.SickLeaves
{
    [QueryProperty(nameof(SickLeaveId), nameof(SickLeaveId))]
    public class CloseSickLeaveWithThirtyOneCodeViewModel : BaseViewModel
    {
        private string _sickLeaveId;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private long? _number;
        private readonly ISickLeaveAppService _sickLeaveAppService;

        public string SickLeaveId
        {
            get => _sickLeaveId;
            set
            {
                _sickLeaveId = value;  
                InitDefaultValues();
            }
        }

        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        public DateTime? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public long? Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }
        
        public AsyncCommand CloseSickLeaveWithCodeCommand { get; }
        

        public CloseSickLeaveWithThirtyOneCodeViewModel()
        {
            _sickLeaveAppService = DependencyService.Get<ISickLeaveAppService>();
            
            CloseSickLeaveWithCodeCommand = new AsyncCommand(OnCloseSickLeaveWithCode);
        }

        private async void InitDefaultValues()
        {
            var lastSickLeave = await _sickLeaveAppService.GetAsync(Guid.Parse(SickLeaveId));
            var lastTermEndDate = lastSickLeave.LastTermEndDate();

            StartDate = lastTermEndDate.AddDays(1);
            EndDate = lastTermEndDate.AddDays(15);
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
    }
}