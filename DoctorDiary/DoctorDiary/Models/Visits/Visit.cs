using System;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Shared.Domain;

namespace DoctorDiary.Models.Visits
{
    public class Visit : FullAuditedAggregateRoot<Guid>
    {
        public virtual DateTime Time { get; protected set; }
        public virtual bool IsCompleted { get; protected set; }
        public virtual Guid PatientCardId { get; protected set; }

        protected Visit()
        {
            
        }

        public Visit(
            Guid id,
            DateTime time,
            PatientCard patientCard) : base(id)
        {
            Time = time;
            IsCompleted = false;
            PatientCardId = patientCard.Id;
        }

        public void ChangePatientCard(PatientCard patientCard)
        {
            PatientCardId = patientCard.Id;
        }

        public void ChangeTime(DateTime time)
        {
            Time = time;
        }

        public void Complete()
        {
            IsCompleted = true;
        }
    }
}