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
            var snilsClearedFromFormat = value.Replace("-", string.Empty);
            
            // if (snilsClearedFromFormat.All(char.IsDigit))
            // {
            //     throw new BusinessException($"СНИЛС содержит некорректные символы: {string.Join("", value.Where(c => !char.IsDigit(c)))}");
            // }
            //
            // if (snilsClearedFromFormat.Length != PatientCardConsts.SnilsLength)
            // {
            //     throw new BusinessException("Неверная длинна СНИЛСа. Длина СНИЛСа должена составлять 11 цифр.");
            // }

            Value = snilsClearedFromFormat;
        }

        public static Snils Empty()
        {
            return new Snils();
        }

        // TODO: override ToString
        public string ToReadableFormat()
        {
            return string.Join('-', new []
            {
                Value[..3],
                Value[3..6],
                Value[6..9],
                Value[9..11]
            });
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}