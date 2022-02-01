using System;
using System.Data.SQLite;
using System.IO;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Models.Reminders;
using DoctorDiary.Models.SickLeaves;
using DoctorDiary.Models.Visits;
using Microsoft.Data.Sqlite;
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
            
            var connectionString = $"Data Source={databasePath}";
            var connection = new SqliteConnection(connectionString);
            connection.CreateCollation("UTF8CI", (x, y) => String.Compare(x, y, false, System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU")));
            connection.CreateFunction("CYR_UPPER", x => x[0] != null ? ((string)x[0]).ToUpper() : null);
            
            optionsBuilder.UseSqlite(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
    
    /// <summary>
    /// Класс реализует COLLATION для русских строк в SQLite
    /// </summary>
    [SQLiteFunction(Name = "UTF8CI", FuncType = FunctionType.Collation, Arguments = 2)]
    public class SQLiteCaseInsensitiveCollation : SQLiteFunction
    {
        private static readonly System.Globalization.CultureInfo _cultureInfo =
            System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU");

        public override int Compare(string x, string y)
        {
            return String.Compare(x, y, false, _cultureInfo);
        }
    }

    /// <summary>
    /// Класс реализует uppercase русских строк для SQLite
    /// </summary>
    [SQLiteFunction(Name = "CYR_UPPER", Arguments = 1, FuncType = FunctionType.Scalar)]
    public class SqLiteCyrHelper : SQLiteFunction
    {
        public override object Invoke(object[] args)
        {
            return args[0] != null ? ((string)args[0]).ToUpper() : null;
        }
    } 
}