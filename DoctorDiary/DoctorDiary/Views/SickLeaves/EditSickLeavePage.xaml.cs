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
        
        private async void Number_OnFocused(object sender, FocusEventArgs e)
        {
            await NumberFrame.ScaleTo(1.01, 100);
        }

        private async void Number_OnUnfocused(object sender, FocusEventArgs e)
        {
            await NumberFrame.ScaleTo(1, 100);
        }

        private async void StartDatePicker_OnFocused(object sender, FocusEventArgs e)
        {
            await StartDate.ScaleTo(1.01, 100);
        }

        private async void StartDatePicker_OnUnfocused(object sender, FocusEventArgs e)
        {
            await StartDate.ScaleTo(1, 100);
        }

        private async void EndDatePicker_OnFocused(object sender, FocusEventArgs e)
        {
            await EndDate.ScaleTo(1.01, 100);
        }

        private async void EndDatePicker_OnUnfocused(object sender, FocusEventArgs e)
        {
            await EndDate.ScaleTo(1, 100);
        }
    }
}