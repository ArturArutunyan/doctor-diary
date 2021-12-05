using DoctorDiary.ViewModels.PatientCards;
using Xamarin.Forms;

namespace DoctorDiary.Views.PatientCards
{
    public partial class PatientCardDetailPage : ContentPage
    {
        public PatientCardDetailPage()
        {
            InitializeComponent();
            BindingContext = new PatientCardDetailViewModel();
        }
    }
}