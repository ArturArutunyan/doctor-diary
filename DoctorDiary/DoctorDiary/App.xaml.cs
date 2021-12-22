using System;
using System.Diagnostics;
using System.Linq;
using DoctorDiary.EntityFrameworkCore;
using DoctorDiary.EntityFrameworkCore.PatientCards;
using DoctorDiary.EntityFrameworkCore.Reminders;
using DoctorDiary.EntityFrameworkCore.SickLeaves;
using DoctorDiary.EntityFrameworkCore.Visits;
using DoctorDiary.Models.PatientCards.ValueObjects;
using DoctorDiary.Services;
using DoctorDiary.Services.MessageBox;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Services.Reminders;
using DoctorDiary.Services.SickLeaves;
using DoctorDiary.Services.Visits;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace DoctorDiary
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            RegisterDependencies();
            InitializeDatabase();
            
            MainPage = new AppShell();
        }

        private void RegisterDependencies()
        {
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<IApplicationDbContext, ApplicationDbContext>();
            DependencyService.Register<IPatientCardAppService, PatientCardAppService>();
            DependencyService.Register<IPatientCardRepository, PatientCardEfCoreRepository>();
            DependencyService.Register<ISickLeaveAppService, SickLeaveAppService>();
            DependencyService.Register<ISickLeaveRepository, SickLeaveEfCoreRepository>();
            DependencyService.Register<IReminderAppService, ReminderAppService>();
            DependencyService.Register<IReminderRepository, ReminderEfCoreRepository>();
            DependencyService.Register<IVisitAppService, VisitAppService>();
            DependencyService.Register<IVisitRepository, VisitEfCoreRepository>();
            DependencyService.Register<IMessageBoxAppService, MessageBoxAppService>();
        }

        private async void InitializeDatabase()
        {
            var database = new ApplicationDbContext().Database;
            var pendingMigrations = await database.GetPendingMigrationsAsync();
            
            if (pendingMigrations.Any())
            {
                await database.MigrateAsync();
            }
            
            var patientCardRepository = DependencyService.Get<IPatientCardRepository>();
            var patientCards = await patientCardRepository.GetListAsync(100, 0);

            if (patientCards.Any())
            {
                foreach (var patientCard in patientCards)
                {
                    var oldPhoneNumber = patientCard.PhoneNumber?.Value;
            
                    if (!string.IsNullOrEmpty(oldPhoneNumber) && oldPhoneNumber.StartsWith('8'))
                    {
                        var phoneNumber = oldPhoneNumber[1..];
                        patientCard.ChangePhoneNumber(new PhoneNumber(PhoneNumber.ToReadableFormat($"7{phoneNumber}")));

                        await patientCardRepository.UpdateAsync(patientCard);
                    }
                }   
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}