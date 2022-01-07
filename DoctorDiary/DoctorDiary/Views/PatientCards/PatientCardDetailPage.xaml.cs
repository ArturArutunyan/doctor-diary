using DoctorDiary.ViewModels.PatientCards;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.PatientCards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientCardDetailPage : ContentPage
    {
        public PatientCardDetailPage()
        {
            InitializeComponent();
            BindingContext = new PatientCardDetailViewModel();
        }
    }
}