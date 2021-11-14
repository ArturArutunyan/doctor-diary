using DoctorDiary.ViewModels.PatientCards;
using Xamarin.Forms;

namespace DoctorDiary.Views.PatientCards
{
    public partial class PatientCardsPage : ContentPage
    {
        PatientCardsViewModel _patientCardsViewModel;
        
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
    }
}