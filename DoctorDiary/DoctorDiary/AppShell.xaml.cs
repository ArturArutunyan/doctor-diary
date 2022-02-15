using DoctorDiary.ViewModels;
using DoctorDiary.Views;
using System;
using System.Collections.Generic;
using DoctorDiary.Views.PatientCards;
using DoctorDiary.Views.SickLeaves;
using DoctorDiary.Views.Visits;
using Xamarin.Forms;

namespace DoctorDiary
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            
            // TODO: Move to localization file
            PatientCards.Title = "Карты";
            Visits.Title = "Вызовы";
            Reminders.Title = "Напоминания";
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(PatientCardDetailPage), typeof(PatientCardDetailPage));
            Routing.RegisterRoute(nameof(NewPatientCardPage), typeof(NewPatientCardPage));
            Routing.RegisterRoute(nameof(EditPatientCardPage), typeof(EditPatientCardPage));
            Routing.RegisterRoute(nameof(OpenSickLeavePage), typeof(OpenSickLeavePage));
            Routing.RegisterRoute(nameof(EditSickLeavePage), typeof(EditSickLeavePage));
            Routing.RegisterRoute(nameof(ExtendSickLeavePage), typeof(ExtendSickLeavePage));
            Routing.RegisterRoute(nameof(CloseSickLeaveWithThirtyOneCodePage), typeof(CloseSickLeaveWithThirtyOneCodePage));
            Routing.RegisterRoute(nameof(CreateDoctorVisitPage), typeof(CreateDoctorVisitPage));
            Routing.RegisterRoute(nameof(EditDoctorVisitPage), typeof(EditDoctorVisitPage));
            Routing.RegisterRoute(nameof(PatientCardsPage), typeof(PatientCardsPage)); // TODO: bullshit
        }
    }
}
