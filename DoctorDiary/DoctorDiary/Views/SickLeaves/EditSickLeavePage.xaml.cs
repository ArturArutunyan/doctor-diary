using DoctorDiary.ViewModels.SickLeaves;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.SickLeaves
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSickLeavePage : ContentPage
    {
        public EditSickLeavePage()
        {
            InitializeComponent();
            BindingContext = new EditSickLeaveViewModel();
        }
    }
}