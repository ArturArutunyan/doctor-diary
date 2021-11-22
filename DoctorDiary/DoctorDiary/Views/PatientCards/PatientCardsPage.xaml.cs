using DoctorDiary.ViewModels.PatientCards;
using Xamarin.Forms;

namespace DoctorDiary.Views.PatientCards
{
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
    }
}