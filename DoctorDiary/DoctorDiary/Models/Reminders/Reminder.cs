using System;
using DoctorDiary.Shared.Domain;

namespace DoctorDiary.Models.Reminders
{
    public class Reminder : FullAuditedAggregateRoot<Guid>
    {
        public string Title { get; }
        public string Description { get; }
        public DateTime? Time { get; }
        public string NavigationLinkOnClick { get; }
        public bool IsActive { get; protected set; }
        public bool IsClosed { get; protected set; }
        
        public Reminder(
            Guid id,
            string title,
            string description,
            DateTime? time,
            string navigationLinkOnClick) : base(id)
        {
            Title = title;
            Description = description;
            Time = time;
            NavigationLinkOnClick = navigationLinkOnClick;
            IsActive = true;
            IsClosed = true;
        }

        public void Disable()
        {
            IsActive = false;
        }
        
        public void Close()
        {
            IsActive = false;
            IsClosed = true;
        }
    }
}