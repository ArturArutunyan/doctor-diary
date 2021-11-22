using DoctorDiary.Services;
using DoctorDiary.Views;
using System;
using DoctorDiary.EntityFrameworkCore;
using DoctorDiary.EntityFrameworkCore.PatientCards;
using DoctorDiary.EntityFrameworkCore.Reminders;
using DoctorDiary.EntityFrameworkCore.SickLeaves;
using DoctorDiary.Services.PatientCards;
using DoctorDiary.Services.Reminders;
using DoctorDiary.Services.SickLeaves;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoctorDiary
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            RegisterDependencies();
            
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
