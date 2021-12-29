using DoctorDiary.Models.PatientCards;
using DoctorDiary.Models.Visits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorDiary.EntityFrameworkCore.Visits
{
    public class VisitEfCoreConfiguration : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Time)
                .HasColumnName(nameof(Visit.Time))
                .HasColumnType("DATE");
            
            builder.Property(x => x.IsCompleted)
                .HasColumnName(nameof(Visit.IsCompleted));
            
            builder.Property(x => x.CreationTime)
                .HasColumnName(nameof(Visit.CreationTime));

            builder.Property(x => x.TypeOfAppeal)
                .HasColumnName(nameof(Visit.TypeOfAppeal));
            
            builder.HasOne<PatientCard>()
                .WithMany()
                .HasForeignKey(x => x.PatientCardId)
                .IsRequired();
        }
    }
}