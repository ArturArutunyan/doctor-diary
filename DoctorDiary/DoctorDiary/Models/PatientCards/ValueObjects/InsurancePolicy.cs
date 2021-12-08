using System.Collections.Generic;
using System.Linq;
using DoctorDiary.Shared.Domain;
using DoctorDiary.Shared.Exceptions;
using DoctorDiary.Shared.PatientCards;

namespace DoctorDiary.Models.PatientCards.ValueObjects
{
    public class InsurancePolicy : ValueObject
    {
        public string Value { get; }

        protected InsurancePolicy()
        {
            
        }
        
        public InsurancePolicy(string value)
        {
            if (value.All(char.IsDigit))
            {
                throw new BusinessException($"Полис должен содержать только цифры: {value}");
            }
            
            if (value.Length != PatientCardConsts.InsurancePolicyLength)
            {
                throw new BusinessException("Длина полиса должна быть 16 цифр: {value}");
            }

            Value = value;
        }

        public static InsurancePolicy Empty()
        {
            return new InsurancePolicy();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}