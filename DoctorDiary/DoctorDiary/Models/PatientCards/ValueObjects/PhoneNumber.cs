using System.Collections.Generic;
using System.Text.RegularExpressions;
using DoctorDiary.Shared.Domain;

namespace DoctorDiary.Models.PatientCards.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string Value { get; }

        public string ReadableFormat => ToReadableFormat();
        
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

        public string ToReadableFormat()
        {
            return string.IsNullOrEmpty(Value) 
                ? string.Empty 
                : $"{Value[..2]} {Value[3..6]} {Value[8..11]} {Value[12..]}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}