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

        private async void SearchPatientCards_Clicked(object sender, EventArgs e)
        {
            var filter = await Navigation.ShowPopupAsync(new SearchPatientCardsPopup(_patientCardsViewModel.Filter));

            if (filter != null)
            {
                _patientCardsViewModel.Filter = filter;
                _patientCardsViewModel.OnAppearing();
            }
        }
    }
}