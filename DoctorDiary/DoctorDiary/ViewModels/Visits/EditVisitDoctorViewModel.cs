using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DoctorDiary.Services.Visits;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.Visits
{
    [QueryProperty(nameof(VisitId), nameof(VisitId))]
    public class EditVisitDoctorViewModel : BaseViewModel
    {
        private string _visitId;
        private Guid _patientCardId;
        private DateTime _time;
        private string _typeOfAppeal;
        private readonly IVisitAppService _visitAppService;

        public string VisitId
        {
            get => _visitId;
            set
            {
                _visitId = value;
                LoadVisit(value);
            }
        }

        public Guid PatientCardId
        {
            get => _patientCardId;
            set => SetProperty(ref _patientCardId, value);
        }

        public DateTime Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }
        
        public string TypeOfAppeal
        {
            get => _typeOfAppeal;
            set => SetProperty(ref _typeOfAppeal, value);
        }
        
        public AsyncCommand EditDoctorVisitCommand { get; }

        public AsyncCommand CancelEditDoctorVisitCommand { get; }

        public EditVisitDoctorViewModel()
        {
            _visitAppService = DependencyService.Get<IVisitAppService>();
            EditDoctorVisitCommand = new AsyncCommand(EditVisit);
            CancelEditDoctorVisitCommand = new AsyncCommand(CancelEditDoctorVisit);
        }

        private async Task EditVisit()
        {
            await _visitAppService.Update(
                patientCardId: PatientCardId, 
                visitId: Guid.Parse(VisitId),
                time: Time,
                typeOfAppeal: TypeOfAppeal);
            
            await Shell.Current.GoToAsync("..");
        }
        
        private async Task CancelEditDoctorVisit()
        {
            await Shell.Current.GoToAsync("..");
        }
        
        private async void LoadVisit(string visitId)
        {
            try
            {
                var visit = await _visitAppService.GetAsync(Guid.Parse(visitId));

                PatientCardId = visit.PatientCardId;
                Time = visit.Time;
                TypeOfAppeal = visit.TypeOfAppeal;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load visit");
            }
        }
    }
}