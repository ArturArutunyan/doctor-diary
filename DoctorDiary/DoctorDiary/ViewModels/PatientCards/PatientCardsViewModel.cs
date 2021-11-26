﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Views.PatientCards;
using MvvmHelpers;
using MvvmHelpers.Commands;
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

        public ObservableRangeCollection<PatientCard> PatientCards { get; }

        public AsyncCommand LoadPatientCardsCommand { get; }

        public AsyncCommand AddPatientCardCommand { get; }

        public AsyncCommand<PatientCard> PatientCardTapped { get; }
        public AsyncCommand LoadMoreCommand { get; }

        public PatientCardsViewModel()
        {
            _patientCardAppService = DependencyService.Get<IPatientCardAppService>();

            PatientCards = new ObservableRangeCollection<PatientCard>();
            
            Title = "Карточки пациентов";
            LoadPatientCardsCommand = new AsyncCommand(LoadPatientCards);
            PatientCardTapped = new AsyncCommand<PatientCard>(OnPatientCardSelected);
            AddPatientCardCommand = new AsyncCommand(AddPatientCard);
            LoadMoreCommand = new AsyncCommand(OnPatientCardsThresholdReached);
        }
        
        private async Task OnPatientCardsThresholdReached()
        {
            if (IsBusy)
                return;
            
            try
            {
                if (RemainingItemsThreshold != -1)
                {
                    var patientCards = await _patientCardAppService.GetLastCreatedPatientCards(
                        takeCount: MaxDefaultPatientCardsTakeCount,
                        skipCount: PatientCards.Count,
                        asNoTracking: true);

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

        private async Task AddPatientCard()
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
                RemainingItemsThreshold = 1;
                
                var patientCards = await _patientCardAppService.GetLastCreatedPatientCards(
                    takeCount: MaxDefaultPatientCardsTakeCount,
                    skipCount: 0,
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