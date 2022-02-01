using System.Linq;
using DoctorDiary.Shared.PatientCards;
using DoctorDiary.Shared.Validations;

namespace DoctorDiary.ViewModels.PatientCards.Validations
{
    public class InsurancePolicyRuleLength : IValidationRule<string>
    {
        public string ValidationMessage { get; } = "Длина полиса должена составлять 16 цифр";
        public bool Check(string value)
        {
            if (string.IsNullOrEmpty(value))
                return true;
            
            var insurancePolicyClearedFromFormat = new string(value.Where(char.IsDigit).ToArray());

            return insurancePolicyClearedFromFormat.Length == PatientCardConsts.InsurancePolicyLength;
        }
    }
}