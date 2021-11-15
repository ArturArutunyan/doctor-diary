using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Views.PatientCards;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.PatientCards
{
    public class PatientCardsViewModel : BaseViewModel
    {
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

        public ObservableCollection<PatientCard> PatientCards { get; }

        public AsyncCommand LoadPatientCardsCommand { get; }

        public AsyncCommand AddPatientCardCommand { get; }

        public AsyncCommand<PatientCard> PatientCardTapped { get; }

        public PatientCardsViewModel()
        {
            Title = "Карточки пациентов";
            PatientCards = new ObservableCollection<PatientCard>();
            
            LoadPatientCardsCommand = new AsyncCommand(async () => await ExecuteLoadPatientCardsCommand());
            PatientCardTapped = new AsyncCommand<PatientCard>(OnPatientCardSelected);
            AddPatientCardCommand = new AsyncCommand(OnAddPatientCard);

            _patientCardAppService = DependencyService.Get<IPatientCardAppService>();
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

        private async Task ExecuteLoadPatientCardsCommand()
        {
            IsBusy = true;

            try
            {
                PatientCards.Clear();
                
                var patientCards = await _patientCardAppService.GetListAsync();
                
                foreach (var patientCard in patientCards)
                {
                    PatientCards.Add(patientCard);
                }
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