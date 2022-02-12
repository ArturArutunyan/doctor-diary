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

        private async void TimeDatePicker_OnFocused(object sender, FocusEventArgs e)
        {
            await Time.ScaleTo(1.01, 100);
        }

        private async void TimeDatePicker_OnUnfocused(object sender, FocusEventArgs e)
        {
            await Time.ScaleTo(1, 100);
        }

        private async void TypeOfAppeal_OnFocused(object sender, FocusEventArgs e)
        {
            await TypeOfAppeal.ScaleTo(1.01, 100);
        }

        private async void TypeOfAppeal_OnUnfocused(object sender, FocusEventArgs e)
        {
            await TypeOfAppeal.ScaleTo(1, 100);
        }
    }
}