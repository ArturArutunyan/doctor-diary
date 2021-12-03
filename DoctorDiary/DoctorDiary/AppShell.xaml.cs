﻿using DoctorDiary.ViewModels;
using DoctorDiary.Views;
using System;
using System.Collections.Generic;
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
            
            // TODO: Move to localization file
            PatientCardsTabTitle.Title = "Карточки";
            RemindersTabTitle.Title = "Напоминания";
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(PatientCardDetailPage), typeof(PatientCardDetailPage));
            Routing.RegisterRoute(nameof(NewPatientCardPage), typeof(NewPatientCardPage));
            Routing.RegisterRoute(nameof(OpenSickLeavePage), typeof(OpenSickLeavePage));
            Routing.RegisterRoute(nameof(ExtendSickLeavePage), typeof(ExtendSickLeavePage));
            Routing.RegisterRoute(nameof(CloseSickLeaveWithThirtyOneCodePage), typeof(CloseSickLeaveWithThirtyOneCodePage));
        }

    }
}
