using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Views.PatientCards;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.PatientCards
{
    public class PatientCardsViewModel : BaseViewModel
    {
        private const int MaxDefaultPatientCardsTakeCount = 5;
        private int _remainingItemsThreshold;
        private PatientCard _selectedPatientCard;

        private readonly IPatientCardAppService _patientCardAppService;

        public int RemainingItemsThreshold
        {
            get => _remainingItemsThreshold; 
            set => SetProperty(ref _remainingItemsThreshold, value);
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

        public PatientCardsFilter Filter { get; set; }
        public ObservableRangeCollection<PatientCard> PatientCards { get; }
        public AsyncCommand LoadPatientCardsCommand { get; }
        public AsyncCommand<PatientCard> PatientCardTapped { get; }
        public AsyncCommand LoadMoreCommand { get; }
        public Command<PatientCard> OpenPhoneDialerCommand { get; }


        public PatientCardsViewModel()
        {
            _patientCardAppService = DependencyService.Get<IPatientCardAppService>();

            PatientCards = new ObservableRangeCollection<PatientCard>();
            
            Title = "Амбулаторные карты";
            Filter = PatientCardsFilter.Default();
            
            LoadPatientCardsCommand = new AsyncCommand(LoadPatientCards);
            PatientCardTapped = new AsyncCommand<PatientCard>(OnPatientCardSelected);
            LoadMoreCommand = new AsyncCommand(OnPatientCardsThresholdReached);
            OpenPhoneDialerCommand = new Command<PatientCard>(OnOpenPhoneDialer);
        }

        private void OnOpenPhoneDialer(PatientCard patientCard)
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
        
        public async Task OnPatientCardsThresholdReached()
        {
            if (IsBusy)
                return;
            
            try
            {
                if (RemainingItemsThreshold != -1)
                {
                    Filter.SkipCount = PatientCards.Count;
                    
                    var patientCards = await _patientCardAppService.GetLastCreatedPatientCards(Filter);

                    if (patientCards.Count != 0)
                    {
                        PatientCards.AddRange(patientCards);  
                    }
                    else
                    {
                        RemainingItemsThreshold = -1;   
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedPatientCard = null;
        }
        
        public async Task LoadPatientCards()
        {
            IsBusy = true;

            try
            {
                PatientCards.Clear();
                RemainingItemsThreshold = 1;

                Filter.SkipCount = 0;
                
                var patientCards = await _patientCardAppService.GetLastCreatedPatientCards(Filter);

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
    }
}