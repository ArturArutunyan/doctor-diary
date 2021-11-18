using DoctorDiary.Models.SickLeaves;
using DoctorDiary.ViewModels.SickLeaves;
using Xamarin.Forms;

namespace DoctorDiary.Views.SickLeaves
{
    public partial class OpenSickLeavePage : ContentPage
    {
        public SickLeave SickLeave { get; set; }

        public OpenSickLeavePage()
        {
            InitializeComponent();
            BindingContext = new OpenSickLeaveViewModel();
        }
    }
}