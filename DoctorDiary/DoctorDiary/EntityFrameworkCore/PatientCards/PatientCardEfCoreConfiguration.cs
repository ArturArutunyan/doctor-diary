using DoctorDiary.Models;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Models.PatientCards.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorDiary.EntityFrameworkCore.PatientCards
{
    public class PatientCardEfCoreConfiguration : IEntityTypeConfiguration<PatientCard>
    {
        public void Configure(EntityTypeBuilder<PatientCard> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .HasColumnName(nameof(PatientCard.FirstName))
                .HasColumnType("NVARCHAR(30)");
            
            builder.Property(x => x.LastName)
                .HasColumnName(nameof(PatientCard.LastName))
                .HasColumnType("NVARCHAR(30)");
            
            builder.Property(x => x.Patronymic)
                .HasColumnName(nameof(PatientCard.Patronymic))
                .HasColumnType("NVARCHAR(30)");

            builder.OwnsOne(x => x.Address, xa =>
            {
                xa.Property(x => x.City)
                    .HasColumnName(nameof(Address.City))
                    .HasColumnType("NVARCHAR(100)");
                
                xa.Property(x => x.Street)
                    .HasColumnName(nameof(Address.Street))
                    .HasColumnType("NVARCHAR(100)");
                
                xa.Property(x => x.Apartment)
                    .HasColumnName(nameof(Address.Apartment))
                    .HasColumnType("NVARCHAR(100)");
                
                xa.Property(x => x.House)
                    .HasColumnName(nameof(Address.House))
                    .HasColumnType("NVARCHAR(100)");
                
                xa.Property(x => x.Entrance)
                    .HasColumnName(nameof(Address.Entrance))
                    .HasColumnType("NVARCHAR(100)");
                
                xa.Property(x => x.Floor)
                    .HasColumnName(nameof(Address.Floor))
                    .HasColumnType("NVARCHAR(100)");
            });

            builder.Property(x => x.Birthday)
                .HasColumnName(nameof(PatientCard.Birthday))
                .HasColumnType("DATE");
            
            builder.OwnsOne(x => x.Snils, xs =>
            {
                xs.Property(x => x.Value)
                    .HasColumnName(nameof(PatientCard.Snils))
                    .HasColumnType("NVARCHAR(11)");
            });

            builder.Property(x => x.Description)
                .HasColumnName(nameof(PatientCard.Description))
                .HasColumnType("NVARCHAR(100)");
            
            builder.OwnsOne(x => x.PhoneNumber, xs =>
            {
                xs.Property(x => x.Value)
                    .HasColumnName(nameof(PatientCard.PhoneNumber))
                    .HasColumnType("NVARCHAR(15)");
            });
            
            builder.Property(x => x.CreationTime)
                .HasColumnName(nameof(PatientCard.CreationTime));
            
            builder.OwnsOne(x => x.InsurancePolicy, xs =>
            {
                xs.Property(x => x.Value)
                    .HasColumnName(nameof(PatientCard.InsurancePolicy))
                    .HasColumnType("NVARCHAR(16)");
            });
            
            builder.Property(x => x.PlaceOfWork)
                .HasColumnName(nameof(PatientCard.PlaceOfWork));
            
            builder.Property(x => x.EmploymentPosition)
                .HasColumnName(nameof(PatientCard.EmploymentPosition));
            
            builder.Property(x => x.Precinct)
                .HasColumnName(nameof(PatientCard.Precinct));
        }
    }
}