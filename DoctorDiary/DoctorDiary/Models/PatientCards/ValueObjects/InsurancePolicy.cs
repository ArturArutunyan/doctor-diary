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
            var insurancePolicyClearedFromFormat = value.Replace("-", string.Empty);
            //
            // if (insurancePolicyClearedFromFormat.All(char.IsDigit))
            // {
            //     throw new BusinessException($"Полис должен содержать только цифры: {value}");
            // }
            //
            // if (insurancePolicyClearedFromFormat.Length != PatientCardConsts.InsurancePolicyLength)
            // {
            //     throw new BusinessException("Длина полиса должна быть 16 цифр: {value}");
            // }

            Value = insurancePolicyClearedFromFormat;
        }

        public static InsurancePolicy Empty()
        {
            return new InsurancePolicy();
        }

        public string ToReadableFormat()
        {
            return string.Join('-', new []
            {
                Value[..4],
                Value[4..8],
                Value[8..12],
                Value[12..16]
            });
        }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}