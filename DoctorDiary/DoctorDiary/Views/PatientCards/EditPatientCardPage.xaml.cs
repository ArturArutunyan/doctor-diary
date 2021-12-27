using System;
using DoctorDiary.ViewModels.PatientCards;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary.Views.PatientCards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPatientCardPage : ContentPage
    {
        public EditPatientCardPage()
        {
            InitializeComponent();
            BindingContext = new EditPatientCardViewModel();
        }
        
        private void CalendarButton_OnClicked(object sender, EventArgs e)
        {
            CalendarDatePicker.Focus();
        }

        private void CalendarDatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            Birthday.Text = CalendarDatePicker.Date.ToString("dd.MM.yyyy");
        }
    }
}