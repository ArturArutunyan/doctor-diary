using DoctorDiary.Models;
using DoctorDiary.Models.PatientCards;
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
            
            builder.Property(x => x.Address)
                .HasColumnName(nameof(PatientCard.Address))
                .HasColumnType("NVARCHAR(100)");
            
            builder.Property(x => x.Birthday)
                .HasColumnName(nameof(PatientCard.Birthday))
                .HasColumnType("DATE");
            
            builder.Property(x => x.Snils)
                .HasColumnName(nameof(PatientCard.Snils))
                .HasColumnType("NVARCHAR(30)");
            
            builder.Property(x => x.Description)
                .HasColumnName(nameof(PatientCard.Description))
                .HasColumnType("NVARCHAR(100)");
        }
    }
}