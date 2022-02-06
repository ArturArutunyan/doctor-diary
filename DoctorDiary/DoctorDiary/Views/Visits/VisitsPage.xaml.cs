using System;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.ViewModels.Visits;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.Visits
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisitsPage : ContentPage
    {
        private readonly VisitsViewModel _visitsViewModel;

        public VisitsPage()
        {
            InitializeComponent();
            BindingContext = _visitsViewModel = new VisitsViewModel();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _visitsViewModel.OnAppearing();
        }

        private async void CalendarButton_OnClicked(object sender, EventArgs e)
        {
            var originalColor = CalendarAltButton.TextColor;
            CalendarAltButton.TextColor = Color.DarkGray;
            await CalendarAltButton.ScaleTo(1.1, 100);
            
            CalendarDatePicker.Focus();
            
            await BackButton.ScaleTo(1, 100);
            CalendarAltButton.TextColor = originalColor;
        }

        private void CalendarDatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            _visitsViewModel.SetDay(e.NewDate);
        }

        private async void BackButton_OnClicked(object sender, EventArgs e)
        {
            var originalColor = BackButton.TextColor;
            BackButton.TextColor = Color.DarkGray;
            await BackButton.ScaleTo(1.1, 100);
            
            await BackButton.ScaleTo(1, 100);
            BackButton.TextColor = originalColor;
        }

        private async void ForwardButton_OnClicked(object sender, EventArgs e)
        {
            var originalColor = ForwardButton.TextColor;
            ForwardButton.TextColor = Color.DarkGray;
            await ForwardButton.ScaleTo(1.1, 100);
            
            await ForwardButton.ScaleTo(1, 100);
            ForwardButton.TextColor = originalColor;
        }

        private async void PhoneButton_OnClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var visitWithPatientCard = (VisitWithPatientCard)button.CommandParameter;

            if (visitWithPatientCard.PatientCard.PhoneNumber != null)
            {
                _visitsViewModel.OpenPhoneDialer(visitWithPatientCard.PatientCard);
            }
            else
            {
                await this.DisplayToastAsync(new ToastOptions()
                {
                    BackgroundColor = Color.White,
                    CornerRadius = new Thickness(7, 7, 0, 0),
                    MessageOptions = new MessageOptions()
                    {
                        Foreground = Color.Red,
                        Font = Font.OfSize("HEB", NamedSize.Medium),
                        Message = $"У пациента не указан номер телефона"
                    }
                });   
            }
        }
    }
}