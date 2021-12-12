using System;
using System.Threading.Tasks;
using DoctorDiary.Services.Visits;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.Visits
{
    [QueryProperty(nameof(PatientCardId), nameof(PatientCardId))]
    public class CreateDoctorVisitViewModel : BaseViewModel
    {
        private string _patientCardId;
        private DateTime _time;
        
        private readonly IVisitAppService _visitAppService;

        public string PatientCardId
        {
            get => _patientCardId;
            set => _patientCardId = value;
        }

        public DateTime Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }
        
        public AsyncCommand CreateDoctorVisitCommand { get; }

        public CreateDoctorVisitViewModel()
        {
            _visitAppService = DependencyService.Get<IVisitAppService>();

            Time = DateTime.Now;
            
            CreateDoctorVisitCommand = new AsyncCommand(CreateDoctorVisit);
        }

        private async Task CreateDoctorVisit()
        {
            await _visitAppService.Create(
                patientCardId: Guid.Parse(PatientCardId), 
                date: Time.Date);
            
            await Shell.Current.GoToAsync("..");
        }
    }
}