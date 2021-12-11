using System;
using DoctorDiary.Shared.Domain;

namespace DoctorDiary.Models.Visits
{
    public class Visit : FullAuditedAggregateRoot<Guid>
    {
        public virtual DateTime Time { get; }
        public virtual bool IsCompleted { get; protected set; }

        protected Visit()
        {
            
        }

        public Visit(
            Guid id,
            DateTime time) : base(id)
        {
            Time = time;
            IsCompleted = false;
        }

        public void Complete()
        {
            IsCompleted = true;
        }
    }
}