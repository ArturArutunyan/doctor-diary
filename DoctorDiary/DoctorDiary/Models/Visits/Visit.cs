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
        public virtual string TypeOfAppeal { get; protected set; }

        protected Visit()
        {
            
        }

        public Visit(
            Guid id,
            DateTime time,
            PatientCard patientCard,
            string typeOfAppeal) : base(id)
        {
            Time = time;
            TypeOfAppeal = typeOfAppeal;
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
        
        public void ChangeTypeOfAppeal(string typeOfAppeal)
        {
            TypeOfAppeal = typeOfAppeal;
        }
    }
}