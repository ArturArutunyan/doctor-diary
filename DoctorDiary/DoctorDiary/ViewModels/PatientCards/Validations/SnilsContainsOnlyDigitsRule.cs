using System.Linq;
using DoctorDiary.Shared.Validations;

namespace DoctorDiary.ViewModels.PatientCards.Validations
{
    public class SnilsContainsOnlyDigitsRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; } = $"СНИЛС содержит некорректные символы";

        public bool Check(string value)
        {
            if (string.IsNullOrEmpty(value))
                return true;
            
            var snilsClearedFromFormat = value.Replace("-", string.Empty);
            
            return snilsClearedFromFormat.All(char.IsDigit);
        }
    }
}