using System.Linq;
using DoctorDiary.Shared.PatientCards;
using DoctorDiary.Shared.Validations;

namespace DoctorDiary.ViewModels.PatientCards.Validations
{
    public class SnilsLengthRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; } = "Длина СНИЛСа должена составлять 11 цифр";
        
        public bool Check(string value)
        {
            if (string.IsNullOrEmpty(value))
                return true;
            
            var snilsClearedFromFormat = new string(value.Where(char.IsDigit).ToArray());

            return snilsClearedFromFormat.Length == PatientCardConsts.SnilsLength;
        }
    }
}