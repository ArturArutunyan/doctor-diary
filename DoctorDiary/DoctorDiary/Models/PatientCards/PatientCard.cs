using System;
using DoctorDiary.Shared.Domain;

namespace DoctorDiary.Models.PatientCards
{
    public class PatientCard : IEntity<Guid>
    {
        public virtual Guid Id { get; }
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual string Patronymic { get; protected set; }
        public virtual string Address { get; protected set; }
        public virtual DateTime Birthday { get; protected set; }
        public virtual string Snils { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual string PhoneNumber { get; protected set; }

        protected PatientCard()
        {
        }

        public PatientCard(
            Guid id,
            string firstName,
            string lastName,
            string patronymic,
            string address,
            DateTime birthday,
            string snils,
            string description,
            string phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Address = address;
            Birthday = birthday;
            Snils = snils;
            Description = description;
            PhoneNumber = phoneNumber;
        }


        public void ChangeFirstName(string firstName)
        {
            FirstName = firstName;
        }
        
        public void ChangeLastName(string lastName)
        {
            LastName = lastName;
        }
        
        public void ChangePatronymic(string patronymic)
        {
            Patronymic = patronymic;
        }
        
        public void ChangeBirthday(DateTime birthday)
        {
            Birthday = birthday;
        }
        
        public void ChangeAddress(string address)
        {
            Address = address;
        }
        
        public void ChangeSnils(string snils)
        {
            Snils = snils;
        }
        
        public void ChangeDescription(string description)
        {
            Description = description;
        }
        
        public void ChangePhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}