using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Models.Visits;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Services.Visits;
using DoctorDiary.ViewModels.PatientCards;
using DoctorDiary.Views.PatientCards;
using DoctorDiary.Views.Visits;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.Visits
{
    public class VisitsViewModel : BaseViewModel
    {
        private string _day;
        private PatientCard _selectedPatientCard;

        private readonly IVisitAppService _visitAppService;
        private readonly IPatientCardAppService _patientCardAppService;

        public string Day
        {
            get => _day;
            set => SetProperty(ref _day, value);
        }
        
        public DateTime? PageDatePicker { get; set; }

        public PatientCard SelectedPatientCard
        {
            get => _selectedPatientCard;
            set
            {
                SetProperty(ref _selectedPatientCard, value);
                OnPatientCardSelected(value);
            }
        }

        public ObservableRangeCollection<VisitWithPatientCard> VisitWithPatientCards { get; }
        
        public AsyncCommand LoadVisitsWithPatientCardsCommand { get; }
        public AsyncCommand<PatientCard> PatientCardTapped { get; set; }
        public AsyncCommand PreviousDayCommand { get; }
        public AsyncCommand NextDayCommand { get; }
        public Command<PatientCard> OpenPhoneDialerCommand { get;}
        public AsyncCommand<Visit> EditVisitCommand { get; }
        public AsyncCommand<Visit> DeleteVisitCommand { get; }

        public VisitsViewModel()
        {
            _visitAppService = DependencyService.Get<IVisitAppService>();
            _patientCardAppService = DependencyService.Get<IPatientCardAppService>();
            
            Day = DateTime.Now.ToString("dd.MM.yyyy");
            PageDatePicker = null;
            VisitWithPatientCards = new ObservableRangeCollection<VisitWithPatientCard>();
            
            LoadVisitsWithPatientCardsCommand = new AsyncCommand(LoadVisitsWithPatientCards);
            PatientCardTapped = new AsyncCommand<PatientCard>(OnPatientCardSelected);
            PreviousDayCommand = new AsyncCommand(PreviousDay);
            NextDayCommand = new AsyncCommand(NextDay);
            OpenPhoneDialerCommand = new Command<PatientCard>(OpenPhoneDialer);
            EditVisitCommand = new AsyncCommand<Visit>(EditVisit);
            DeleteVisitCommand = new AsyncCommand<Visit>(DeleteVisit);
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedPatientCard = null;
        }
        
        private async Task LoadVisitsWithPatientCards()
        {
            IsBusy = true;

            try
            {
                VisitWithPatientCards.Clear();

                var visits = await _visitAppService.VisitsByDate(date: DateTime.ParseExact(s: Day, format: "dd.MM.yyyy", provider: null), asNoTracking: true);
                var patientCards = await _patientCardAppService.PatientCardsByVisits(date: DateTime.ParseExact(s: Day, format: "dd.MM.yyyy", provider: null), asNoTracking: true);
                var visitsWithPatientCards = visits.Join(patientCards,
                    v => v.PatientCardId,
                    p => p.Id,
                    (v, p) => new VisitWithPatientCard(v, p));
                
                VisitWithPatientCards.AddRange(visitsWithPatientCards);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnPatientCardSelected(PatientCard patientCard)
        {
            if (patientCard == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(PatientCardDetailPage)}?{nameof(PatientCardDetailViewModel.PatientCardId)}={patientCard.Id}");
        }

        private async Task PreviousDay()
        {
            if (IsBusy)
                return;

            Day = DateTime.ParseExact(s: Day, format: "dd.MM.yyyy", provider: null)
                .AddDays(-1)
                .ToString("dd.MM.yyyy");
            IsBusy = true;
        }

        private async Task NextDay()
        {
            if (IsBusy)
                return;
            
            Day = DateTime.ParseExact(s: Day, format: "dd.MM.yyyy", provider: null)
                .AddDays(1)
                .ToString("dd.MM.yyyy");
            IsBusy = true;
        }
        
        public void SetDay(DateTime time)
        {
            if (IsBusy)
                return;

            Day = time.ToString("dd.MM.yyyy");;
            IsBusy = true;
        }

        public void OpenPhoneDialer(PatientCard patientCard)
        {
            try
            {
                if (!string.IsNullOrEmpty(patientCard.PhoneNumber.Value))
                {
                    PhoneDialer.Open(Models.PatientCards.ValueObjects.PhoneNumber.ClearFromFormat(patientCard.PhoneNumber.Value));
                }
            }
            catch (ArgumentNullException anEx)
            {
                Console.WriteLine(anEx);
            }
            catch (FeatureNotSupportedException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        private async Task EditVisit(Visit visit)
        {
            if (visit == null)
                return;
            
            await Shell.Current.GoToAsync($"{nameof(EditDoctorVisitPage)}?VisitId={visit.Id}");
        }

        private async Task DeleteVisit(Visit visit)
        {
            if (visit == null)
                return;

            await _visitAppService.Delete(visit.Id);
            VisitWithPatientCards.Remove(VisitWithPatientCards.Single(x => x.Visit.Id == visit.Id));
        }
    }

    // TODO: remove
    public class VisitWithPatientCard
    {
        public Visit Visit { get; }
        public PatientCard PatientCard { get; }

        public VisitWithPatientCard(Visit visit, PatientCard patientCard)
        {
            Visit = visit;
            PatientCard = patientCard;
        }
    }
}