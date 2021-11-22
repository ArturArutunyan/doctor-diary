using DoctorDiary.Models.Reminders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorDiary.EntityFrameworkCore.Reminders
{
    public class ReminderEfCoreConfiguration : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasColumnName(nameof(Reminder.Title))
                .IsRequired();
            
            builder.Property(x => x.Description)
                .HasColumnName(nameof(Reminder.Description))
                .IsRequired();

            builder.Property(x => x.Time)
                .HasColumnName(nameof(Reminder.Time));
            
            builder.Property(x => x.NavigationLinkOnClick)
                .HasColumnName(nameof(Reminder.NavigationLinkOnClick))
                .IsRequired();
            
            builder.Property(x => x.IsActive)
                .HasColumnName(nameof(Reminder.IsActive))
                .IsRequired();
            
            builder.Property(x => x.IsClosed)
                .HasColumnName(nameof(Reminder.IsClosed))
                .IsRequired();
            
            builder.Property(x => x.CreationTime)
                .HasColumnName(nameof(Reminder.CreationTime));
        }
    }
}