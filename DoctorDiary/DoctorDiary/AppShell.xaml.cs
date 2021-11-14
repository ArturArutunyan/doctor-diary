using DoctorDiary.ViewModels;
using DoctorDiary.Views;
using System;
using System.Collections.Generic;
using DoctorDiary.Models;
using DoctorDiary.Views.PatientCards;
using Xamarin.Forms;

namespace DoctorDiary
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(PatientCardDetailPage), typeof(PatientCardDetailPage));
            Routing.RegisterRoute(nameof(NewPatientCardPage), typeof(NewPatientCardPage));
        }
    }
}
