using System;

namespace DoctorDiary.Shared.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
            
        }
    }
}