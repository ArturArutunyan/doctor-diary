using DoctorDiary.ViewModels;
using DoctorDiary.Views;
using System;
using System.Collections.Generic;
using DoctorDiary.Models;
using DoctorDiary.Views.PatientCards;
using DoctorDiary.Views.SickLeaves;
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

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(PatientCardDetailPage), typeof(PatientCardDetailPage));
            Routing.RegisterRoute(nameof(NewPatientCardPage), typeof(NewPatientCardPage));
            Routing.RegisterRoute(nameof(OpenSickLeavePage), typeof(OpenSickLeavePage));
            Routing.RegisterRoute(nameof(CloseSickLeaveWithCodePage), typeof(CloseSickLeaveWithCodePage));
        }
    }
}
