using System;
using DoctorDiary.ViewModels.PatientCards;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.PatientCards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientCardsPage : ContentPage
    {
        private bool _searchButtonIsTapped = false;
        private bool _addButtonIsTapped = false;
        
        readonly PatientCardsViewModel _patientCardsViewModel;
        public PatientCardsPage()
        {
            InitializeComponent();
            BindingContext = _patientCardsViewModel = new PatientCardsViewModel();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _patientCardsViewModel.OnAppearing();
        }

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            if(_searchButtonIsTapped)
                return;

            _searchButtonIsTapped = true;
            
            var filter = await Navigation.ShowPopupAsync(new SearchPatientCardsPopup(_patientCardsViewModel.Filter));

            if (filter != null)
            {
                _patientCardsViewModel.Filter = filter;
                _patientCardsViewModel.OnAppearing();
            }
            
            _searchButtonIsTapped = false;
        }

        private async void AddButton_OnClicked(object sender, EventArgs e)
        {
            if(_addButtonIsTapped)
                return;

            _addButtonIsTapped = true;
            
            await Shell.Current.GoToAsync(nameof(NewPatientCardPage));

            _addButtonIsTapped = false;
        }
    }
}