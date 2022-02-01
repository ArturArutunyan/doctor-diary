using System.Linq;
using DoctorDiary.Shared.PatientCards;
using DoctorDiary.Shared.Validations;

namespace DoctorDiary.ViewModels.PatientCards.Validations
{
    public class PhoneNumberRule : IValidationRule<string>
    {
        public string ValidationMessage { get; } = "Некорретный номер телефона";
        public bool Check(string value)
        {
            if (string.IsNullOrEmpty(value))
                return true;
            
            var phoneNumberClearedFromFormat = new string(value.Where(char.IsDigit).ToArray());

            return phoneNumberClearedFromFormat.Length == PatientCardConsts.PhoneNumberLength;
        }
    }
}