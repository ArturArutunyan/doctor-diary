using System;
using DoctorDiary.Models.PatientCards.ValueObjects;
using DoctorDiary.Shared.Domain;

namespace DoctorDiary.Models.PatientCards
{
    public class PatientCard : FullAuditedAggregateRoot<Guid>
    {
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual string Patronymic { get; protected set; }
        public virtual Address Address { get; protected set; }
        public virtual DateTime Birthday { get; protected set; }
        public virtual Snils Snils { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual PhoneNumber PhoneNumber { get; protected set; }
        public virtual string Gender { get; protected set; }
        public virtual InsurancePolicy InsurancePolicy { get; protected set; }
        public virtual string PlaceOfWork { get; protected set; }
        public virtual int Precinct { get; protected set; }
        
        protected PatientCard()
        {
        }

        public PatientCard(
            Guid id,
            string firstName,
            string lastName,
            string patronymic,
            Address address,
            DateTime birthday,
            Snils snils,
            string description,
            PhoneNumber phoneNumber,
            string gender,
            InsurancePolicy insurancePolicy,
            string placeOfWork,
            int precinct) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Address = address;
            Birthday = birthday;
            Snils = snils;
            Description = description;
            PhoneNumber = phoneNumber;
            Gender = gender;
            InsurancePolicy = insurancePolicy;
            PlaceOfWork = placeOfWork;
            Precinct = precinct;
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
        
        public void ChangeAddress(Address address)
        {
            Address = address;
        }
        
        public void ChangeSnils(Snils snils)
        {
            Snils = snils;
        }
        
        public void ChangeDescription(string description)
        {
            Description = description;
        }
        
        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
        
        public void ChangeGender(string gender)
        {
            Gender = gender;
        }
        
        public void ChangeInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            InsurancePolicy = insurancePolicy;
        }
        
        public void ChangePlaceOfWork(string placeOfWork)
        {
            PlaceOfWork = placeOfWork;
        }
        
        public void ChangePrecinct(int precinct)
        {
            Precinct = precinct;
        }
    }
}