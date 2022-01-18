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
        public string Entrance { get; }
        public string Floor { get; }
        public string InShortFormat => ToShortFormat();

        protected Address()
        {
            
        }
        
        public Address(
            string city, 
            string street, 
            string apartment, 
            string house,
            string entrance,
            string floor)
        {
            City = city;
            Street = street;
            Apartment = apartment;
            House = house;
            Entrance = entrance;
            Floor = floor;
        }

        public override string ToString()
        {
            var formattedCity = string.IsNullOrEmpty(City)
                ? string.Empty
                : $"г.{City}";
            var formattedStreet = string.IsNullOrEmpty(Street)
                ? string.Empty
                : $"ул.{Street}";
            var formattedHouse = string.IsNullOrEmpty(House)
                ? string.Empty
                : $"дом {House}";
            var formattedEntrance = string.IsNullOrEmpty(Entrance)
                ? string.Empty
                : $"{Entrance} подъезд";
            var formattedFloor = string.IsNullOrEmpty(Floor)
                ? string.Empty
                : $"{Floor} этаж";
            var formattedApartment = string.IsNullOrEmpty(Apartment)
                ? string.Empty
                : $"кв. {Apartment}";
            
            return string.Join(", ", new[]
            {
                formattedCity,
                formattedStreet,
                formattedHouse,
                formattedEntrance,
                formattedFloor,
                formattedApartment
            }.Where(x => !string.IsNullOrEmpty(x)));
        }

        public string ToShortFormat()
        {
            var formattedStreet = string.IsNullOrEmpty(Street)
                ? string.Empty
                : $"ул.{Street}";
            var formattedHouse = string.IsNullOrEmpty(House)
                ? string.Empty
                : $"д.{House}";
            var formattedEntrance = string.IsNullOrEmpty(Entrance)
                ? string.Empty
                : $"{Entrance} подъезд";
            var formattedFloor = string.IsNullOrEmpty(Floor)
                ? string.Empty
                : $"{Floor} этаж";
            var formattedApartment = string.IsNullOrEmpty(Apartment)
                ? string.Empty
                : $"кв.{Apartment}";
            
            return string.Join(", ", new[]
            {
                formattedStreet,
                formattedHouse,
                formattedEntrance,
                formattedFloor,
                formattedApartment
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