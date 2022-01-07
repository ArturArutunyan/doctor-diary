using DoctorDiary.ViewModels.Visits;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.Visits
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditDoctorVisitPage : ContentPage
    {
        public EditDoctorVisitPage()
        {
            InitializeComponent();
            BindingContext = new EditVisitDoctorViewModel();
        }
    }
}