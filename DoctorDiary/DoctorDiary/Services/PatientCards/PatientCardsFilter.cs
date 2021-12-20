using System;

namespace DoctorDiary.Services.PatientCards
{
    public class PatientCardsFilter
    {
        public int TakeCount { get; set; }
        public int SkipCount { get; set; }
        public bool AsNoTracking { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Patronymic { get; set; } 
        public string City { get; set; } 
        public string Street { get; set; } 
        public string Apartment { get; set; }
        public string House { get; set; } 
        public DateTime? Birthday { get; set; } 
        public string Snils { get; set; } 
        public string Description { get; set; }
        public string PhoneNumber { get; set; } 
        public string Gender { get; set; } 
        public string InsurancePolicy { get; set; } 
        public string PlaceOfWork { get; set; }
        public int? Precinct { get; set; }

        public static PatientCardsFilter Default(int maxDefaultPatientCardsTakeCount)
        {
            return new PatientCardsFilter()
            {
                TakeCount = maxDefaultPatientCardsTakeCount,
                SkipCount = 0,
                AsNoTracking = true
            };
        }
    }
}