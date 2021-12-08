using System.Collections.Generic;
using System.Linq;
using DoctorDiary.Shared.Domain;
using DoctorDiary.Shared.Exceptions;
using DoctorDiary.Shared.PatientCards;

namespace DoctorDiary.Models.PatientCards.ValueObjects
{
    public class Snils : ValueObject
    {
        public string Value { get; }

        protected Snils()
        {
        }

        public Snils(string value)
        {
            if (value.All(char.IsDigit))
            {
                throw new BusinessException($"СНИЛС содержит некорректные символы: {value.Where(c => !char.IsDigit(c))}");
            }
            
            if (value.Length != PatientCardConsts.SnilsLength)
            {
                throw new BusinessException("Неверная длинна СНИЛСа. Длина СНИЛСа должена составлять 11 цифр.");
            }

            Value = value;
        }

        public static Snils Empty()
        {
            return new Snils();
        }

        public override string ToString()
        {
            return Value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;;
        }
    }
}