namespace DoctorDiary.Shared.Validations
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; }
        
        bool Check(T value);
    }
}