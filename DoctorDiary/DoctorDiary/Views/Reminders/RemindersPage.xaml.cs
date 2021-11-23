using DoctorDiary.ViewModels.Reminders;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.Reminders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RemindersPage : ContentPage
    {
        private readonly RemindersViewModel _remindersViewModel;

        public RemindersPage()
        {
            InitializeComponent();
            BindingContext = _remindersViewModel = new RemindersViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _remindersViewModel.OnAppearing();
        }
    }
}