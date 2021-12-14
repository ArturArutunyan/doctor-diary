using System.Collections.Generic;
using System.Text.RegularExpressions;
using DoctorDiary.Shared.Domain;

namespace DoctorDiary.Models.PatientCards.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string Value { get; }

        protected PhoneNumber()
        {
        }

        public PhoneNumber(string value)
        {
            Value = value;
        }

        public static string ClearFromFormat(string value)
        {
            var pattern = new Regex(@"\D");
            return $"+{pattern.Replace(value, string.Empty)}";
        }

        public static string ToReadableFormat(string value)
        {
            return string.IsNullOrEmpty(value) 
                ? string.Empty 
                : $"+{value[0]}({value[1..4]})-{value[4..7]}-{value[7..9]}{value[9..11]}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}