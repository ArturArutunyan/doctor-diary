using DoctorDiary.EntityFrameworkCore;
using DoctorDiary.EntityFrameworkCore.PatientCards;
using DoctorDiary.EntityFrameworkCore.SickLeaves;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Models.Reminders;
using DoctorDiary.Models.SickLeaves;
using DoctorDiary.Models.Visits;
using Microsoft.EntityFrameworkCore;

namespace DoctorDiary.DbMigrator
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<PatientCard> PatientCards { get; }
        public DbSet<SickLeave> SickLeaves { get; }
        public DbSet<Reminder> Reminders { get; }
        public DbSet<Visit> Visits { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename=DoctorDiaryApp.db");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IApplicationDbContext).Assembly);
        }
    }
}