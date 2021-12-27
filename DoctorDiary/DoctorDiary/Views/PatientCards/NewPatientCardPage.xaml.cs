using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorDiary.Models;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.ViewModels.PatientCards;
using Xamarin.Forms;

namespace DoctorDiary.Views.PatientCards
{
    public partial class NewPatientCardPage : ContentPage
    {
        public NewPatientCardPage()
        {
            InitializeComponent();
            BindingContext = new NewPatientCardViewModel();
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