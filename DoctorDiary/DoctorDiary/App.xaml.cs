using System;
using System.Diagnostics;
using System.Linq;
using DoctorDiary.EntityFrameworkCore;
using DoctorDiary.EntityFrameworkCore.PatientCards;
using DoctorDiary.EntityFrameworkCore.Reminders;
using DoctorDiary.EntityFrameworkCore.SickLeaves;
using DoctorDiary.Services;
using DoctorDiary.Services.MessageBox;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Services.Reminders;
using DoctorDiary.Services.SickLeaves;
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