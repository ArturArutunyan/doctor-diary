using System.Threading;
using System.Threading.Tasks;
using DoctorDiary.Models;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Models.Reminders;
using DoctorDiary.Models.SickLeaves;
using DoctorDiary.Models.Visits;
using Microsoft.EntityFrameworkCore;

namespace DoctorDiary.EntityFrameworkCore
{
    // TODO: Move to shared
    public interface IEfCoreDbContext
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public interface IApplicationDbContext : IEfCoreDbContext
    {
        DbSet<PatientCard> PatientCards { get; }
        DbSet<SickLeave> SickLeaves { get; }
        DbSet<Reminder> Reminders { get; }
        DbSet<Visit> Visits { get; }
    }
}