using System;
using DoctorDiary.Models.Reminders;
using DoctorDiary.ViewModels.Reminders;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.Reminders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RemindersPage : ContentPage
    {
        private static readonly Color TextColor = Color.FromHex(Application.Current.Resources["TextColor"].ToString());
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

        private async void CrossButton_OnClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var originalColor = button.TextColor;
            button.TextColor = TextColor;
            await button.ScaleTo(1.1, 50);
            await button.ScaleTo(1, 50);
            button.TextColor = originalColor;

            var commandParameter = (Reminder)button.CommandParameter;
            await _remindersViewModel.CloseReminder(commandParameter);
        }
    }
}