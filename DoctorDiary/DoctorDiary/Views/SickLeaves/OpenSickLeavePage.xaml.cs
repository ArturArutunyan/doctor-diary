using DoctorDiary.Models.SickLeaves;
using DoctorDiary.ViewModels.SickLeaves;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.SickLeaves
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenSickLeavePage : ContentPage
    {
        public OpenSickLeavePage()
        {
            InitializeComponent();
            BindingContext = new OpenSickLeaveViewModel();
        }
    }
}