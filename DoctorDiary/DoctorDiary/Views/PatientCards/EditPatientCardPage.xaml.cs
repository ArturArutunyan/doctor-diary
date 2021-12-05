using DoctorDiary.ViewModels.PatientCards;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.PatientCards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPatientCardPage : ContentPage
    {
        public EditPatientCardPage()
        {
            InitializeComponent();
            BindingContext = new EditPatientCardViewModel();
        }
    }
}