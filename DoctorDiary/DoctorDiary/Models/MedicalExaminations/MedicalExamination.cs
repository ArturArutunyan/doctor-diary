using System;
using DoctorDiary.Shared.Domain;
using DoctorDiary.Shared.MedicalExaminations;

namespace DoctorDiary.Models.MedicalExaminations
{
    public class MedicalExamination : FullAuditedAggregateRoot<Guid>
    {
        public DateTime Time { get; }
        public string Complaints { get; }
        public string Anamnesis { get; }
        public string PatientCondition { get; }
        public string MindCondition { get; }
        public string SkinIntegument { get; }
        public string WhenYawning { get; }
        public string NasalBreathing { get; }
        public string HeartTones { get; }
        public int NumberOfHeartContractionsInMin { get; }
        public int PulseInMin { get; }
        public string Wheezing { get; }
        
        public string Diagnosis { get; }


        protected MedicalExamination()
        {
            
        }

        public MedicalExamination(
            Guid id,
            DateTime time,
            string complaints,
            string anamnesis, 
            string patientCondition,
            string mindCondition,
            string skinIntegument,
            string whenYawning,
            string nasalBreathing,
            string heartTones,
            int numberOfHeartContractionsInMin,
            int pulseInMin,
            string wheezing,
            string diagnosis) : base(id)
        {
            Time = time;
            Complaints = complaints;
            Anamnesis = anamnesis;
            PatientCondition = patientCondition;
            MindCondition = mindCondition;
            SkinIntegument = skinIntegument;
            WhenYawning = whenYawning;
            NasalBreathing = nasalBreathing;
            HeartTones = heartTones;
            NumberOfHeartContractionsInMin = numberOfHeartContractionsInMin;
            PulseInMin = pulseInMin;
            Wheezing = wheezing;
            Diagnosis = diagnosis;
        }
    }
}