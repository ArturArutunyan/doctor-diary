using System;
using System.Threading.Tasks;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using DoctorDiary.Services.Reminders;
using DoctorDiary.Services.SickLeaves;
using DoctorDiary.ViewModels.PatientCards;
using DoctorDiary.Views.PatientCards;
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
        private readonly IReminderAppService _reminderAppService;

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

        public AsyncCommand OpenSickLeaveAsyncCommand { get; }

        public OpenSickLeaveViewModel()
        {
            _sickLeaveAppService = DependencyService.Get<ISickLeaveAppService>();
            _reminderAppService = DependencyService.Get<IReminderAppService>();

            OpenSickLeaveAsyncCommand = new AsyncCommand(OnOpenSickLeave);

            InitDefaultProperties();
        }

        private async Task OnOpenSickLeave()
        {
            var route = $"{nameof(PatientCardDetailPage)}?{nameof(PatientCardDetailViewModel.PatientCardId)}={PatientCardId}";
            
            await _sickLeaveAppService.OpenSickLeave(
                patientCardId: Guid.Parse(PatientCardId), 
                number: Number,
                term: Term.Create(startDate: StartDate, endDate: EndDate));

            // TODO: add fio
            await _reminderAppService.Create(
                title: "Заканчивается больничный лист",
                description: "Нажмите на это напоминание, чтобы перейти в карточку пациента",
                navigationLinkOnClick: route,
                time: EndDate);
                
            await Shell.Current.GoToAsync(route);
        }

        private void InitDefaultProperties()
        {
            StartDate = DateTime.Today.Date;
            EndDate = StartDate.AddDays(14);
        }
    }
}