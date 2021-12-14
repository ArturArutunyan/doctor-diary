using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Services.Visits;
using DoctorDiary.ViewModels.PatientCards;
using DoctorDiary.Views.PatientCards;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.Visits
{
    public class VisitsViewModel : BaseViewModel
    {
        private DateTime _day;
        private PatientCard _selectedPatientCard;
        
        private readonly IVisitAppService _visitAppService;
        private readonly IPatientCardAppService _patientCardAppService;

        public DateTime Day
        {
            get => _day.Date;
            set => SetProperty(ref _day, value);
        }

        public PatientCard SelectedPatientCard
        {
            get => _selectedPatientCard;
            set
            {
                SetProperty(ref _selectedPatientCard, value);
                OnPatientCardSelected(value);
            }
        }
        
        public ObservableRangeCollection<PatientCard> PatientCards { get; }

        public AsyncCommand LoadPatientCardsCommand { get; }
        public AsyncCommand<PatientCard> PatientCardTapped { get; set; }
        public AsyncCommand PreviousDayCommand { get; }
        public AsyncCommand NextDayCommand { get; }
        public Command<PatientCard> OpenPhoneDialerCommand { get;}

        public VisitsViewModel()
        {
            _visitAppService = DependencyService.Get<IVisitAppService>();
            _patientCardAppService = DependencyService.Get<IPatientCardAppService>();

            Title = "Вызовы";
            Day = DateTime.Now;
            PatientCards = new ObservableRangeCollection<PatientCard>();

            LoadPatientCardsCommand = new AsyncCommand(LoadPatientCards);
            PatientCardTapped = new AsyncCommand<PatientCard>(OnPatientCardSelected);
            PreviousDayCommand = new AsyncCommand(PreviousDay);
            NextDayCommand = new AsyncCommand(NextDay);
            OpenPhoneDialerCommand = new Command<PatientCard>(OpenPhoneDialer);
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedPatientCard = null;
        }
        
        private async Task LoadPatientCards()
        {
            IsBusy = true;

            try
            {
                PatientCards.Clear();
                
                var patientCards = await _patientCardAppService.PatientCardsByVisits(date: Day, asNoTracking: true);

                PatientCards.AddRange(patientCards);
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

            Day = Day.AddDays(-1);
            IsBusy = true;
        }

        private async Task NextDay()
        {
            if (IsBusy)
                return;

            Day = Day.AddDays(1);
            IsBusy = true;
        }
        
        public void SetDay(DateTime time)
        {
            if (IsBusy)
                return;

            Day = time;
            IsBusy = true;
        }

        private void OpenPhoneDialer(PatientCard patientCard)
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
    }
}