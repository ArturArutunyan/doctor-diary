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

        private void SearchPatientCards_Clicked(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new SearchPatientCardsPopup(_patientCardsViewModel));
        }
    }
}