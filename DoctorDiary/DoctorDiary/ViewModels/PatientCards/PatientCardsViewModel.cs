using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Views.PatientCards;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace DoctorDiary.ViewModels.PatientCards
{
    public class PatientCardsViewModel : BaseViewModel
    {
        private const int PageSize = 10;
        private PatientCard _selectedPatientCard;
        private readonly IPatientCardAppService _patientCardAppService;

        public PatientCard SelectedPatientCard
        {
            get => _selectedPatientCard;
            set
            {
                SetProperty(ref _selectedPatientCard, value);
                OnPatientCardSelected(value);
            }
        }

        public InfiniteScrollCollection<PatientCard> PatientCards { get; }

        public AsyncCommand LoadPatientCardsCommand { get; }

        public AsyncCommand AddPatientCardCommand { get; }

        public AsyncCommand<PatientCard> PatientCardTapped { get; }

        public PatientCardsViewModel()
        {
            _patientCardAppService = DependencyService.Get<IPatientCardAppService>();
            
            PatientCards = new InfiniteScrollCollection<PatientCard>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;
                    
                    var patientCards = await _patientCardAppService.GetListAsync(
                        takeCount: PageSize, 
                        skipCount: PatientCards.Count,
                        asNoTracking: true);
                    
                    IsBusy = false;
                    
                    return patientCards;
                },
                OnCanLoadMore = () => PatientCards.Count < 100
            };
            
            Title = "Карточки пациентов";
            LoadPatientCardsCommand = new AsyncCommand(LoadPatientCards);
            PatientCardTapped = new AsyncCommand<PatientCard>(OnPatientCardSelected);
            AddPatientCardCommand = new AsyncCommand(OnAddPatientCard);
        }

        private async Task OnAddPatientCard()
        {
            await Shell.Current.GoToAsync(nameof(NewPatientCardPage));
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

                var patientCards = await _patientCardAppService.GetLastCreatedPatientCards(
                    takeCount: 10,
                    asNoTracking: true);

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