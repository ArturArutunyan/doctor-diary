using DoctorDiary.ViewModels.Visits;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.Visits
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateDoctorVisitPage : ContentPage
    {
        public CreateDoctorVisitPage()
        {
            InitializeComponent();
            BindingContext = new CreateDoctorVisitViewModel();
        }
    }
}