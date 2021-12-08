using System.Collections.Generic;
using System.Linq;
using DoctorDiary.Shared.Domain;

namespace DoctorDiary.Models.PatientCards.ValueObjects
{
    public class Address : ValueObject
    {
        public string City { get; }
        public string Street { get; }
        public string Apartment { get; }
        public string House { get; }

        protected Address()
        {
            
        }
        
        public Address(string city, string street, string apartment, string house)
        {
            City = city;
            Street = street;
            Apartment = apartment;
            House = house;
        }

        public override string ToString()
        {
            return string.Join(", ", new[]
            {
                City,
                Street,
                Apartment,
                House
            }.Where(x => !string.IsNullOrEmpty(x)));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return City;
            yield return Street;
            yield return Apartment;
            yield return House;
        }
    }
}