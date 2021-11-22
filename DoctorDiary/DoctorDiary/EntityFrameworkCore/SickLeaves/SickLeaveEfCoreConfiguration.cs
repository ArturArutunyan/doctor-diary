using System;
using DoctorDiary.Models;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Models.SickLeaves;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorDiary.EntityFrameworkCore.SickLeaves
{
    public class SickLeaveEfCoreConfiguration : IEntityTypeConfiguration<SickLeave>
    {
        public void Configure(EntityTypeBuilder<SickLeave> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Number)
                .HasColumnName(nameof(SickLeave.Number))
                .IsRequired();
            
            builder.Property(x => x.PatientCardId)
                .HasColumnName(nameof(SickLeave.PatientCardId))
                .IsRequired();

            builder.HasOne<PatientCard>()
                .WithMany()
                .HasForeignKey(x => x.PatientCardId)
                .IsRequired();
            
            builder.OwnsMany(x => x.Terms, x =>
            {
                x.ToTable("TermsOfSickLeaves");
                x.WithOwner().HasForeignKey("SickLeaveId");
                x.Property<Guid>("Id").ValueGeneratedOnAdd();
                x.HasKey("Id");
                x.Property(t => t.StartDate)
                    .HasColumnName(nameof(Term.StartDate))
                    .IsRequired();
                x.Property(t => t.EndDate)
                    .HasColumnName(nameof(Term.EndDate))
                    .IsRequired();
            });

            builder.Navigation(x => x.Terms)
                .AutoInclude();
            
            builder.Metadata.FindNavigation(nameof(SickLeave.Terms))
                .SetPropertyAccessMode(PropertyAccessMode.Field);
            
            builder.Property(x => x.CreationTime)
                .HasColumnName(nameof(SickLeave.CreationTime));
        }
    }
}