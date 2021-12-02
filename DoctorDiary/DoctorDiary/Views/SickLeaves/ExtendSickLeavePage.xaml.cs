using DoctorDiary.Models.SickLeaves;
using DoctorDiary.ViewModels.SickLeaves;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.SickLeaves
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExtendSickLeavePage : ContentPage
    {
        public SickLeave SickLeave { get; set; }
        
        public ExtendSickLeavePage()
        {
            InitializeComponent();
            BindingContext = new ExtendSickLeaveViewModel();
        }
    }
}