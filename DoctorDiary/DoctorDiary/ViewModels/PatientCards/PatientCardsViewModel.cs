using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using DoctorDiary.Models;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Views;
using DoctorDiary.Views.PatientCards;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace DoctorDiary.ViewModels.PatientCards
{
    public class PatientCardsViewModel : BaseViewModel
    {
        private PatientCard _selectedPatientCard;
        private readonly IPatientCardAppService _patientCardAppService;

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

        async Task ExecuteLoadPatientCardsCommand()
        {
            IsBusy = true;

            try
            {
                PatientCards.Clear();
                
                var patientCards = await _patientCardAppService.GetListAsync();
                
                foreach (var item in patientCards)
                {
                    PatientCards.Add(item);
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

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedPatientCard = null;
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

        private async Task OnPatientCardSelected(PatientCard patientCard)
        {
            if (patientCard == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(PatientCardDetailPage)}?{nameof(PatientCardDetailViewModel.PatientCardId)}={patientCard.Id}");
        }
    }
}