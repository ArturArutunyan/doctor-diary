using System;

namespace DoctorDiary.Shared.Validations
{
    public class DateTimeAsStringRule : IValidationRule<string>
    {
        public string ValidationMessage { get; } = "Некорректная дата";
        public bool Check(string value)
        {
            if (string.IsNullOrEmpty(value))
                return true;
            
            try
            {
                var dateTime = DateTime.Parse(value);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}