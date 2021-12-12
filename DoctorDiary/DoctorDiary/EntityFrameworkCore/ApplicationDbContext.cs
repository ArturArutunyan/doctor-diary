using System;
using System.IO;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Models.Reminders;
using DoctorDiary.Models.SickLeaves;
using DoctorDiary.Models.Visits;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace DoctorDiary.EntityFrameworkCore
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<PatientCard> PatientCards { get; }
        public DbSet<SickLeave> SickLeaves { get; }
        public DbSet<Reminder> Reminders { get; }
        public DbSet<Visit> Visits { get; }

        public ApplicationDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string databaseName = "doctor-diary-app.db"; 
            var databasePath = Device.RuntimePlatform switch
            {
                Device.iOS => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", databaseName),
                Device.Android => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName),
                _ => throw new NotImplementedException("Platform not supported")
            };
            
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}