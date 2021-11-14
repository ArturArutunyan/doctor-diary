using System;
using DoctorDiary.Models;
using DoctorDiary.Models.PatientCards;

namespace DoctorDiary.EntityFrameworkCore.PatientCards
{
    public interface IPatientCardRepository : IRepository<PatientCard, Guid>
    {
        
    }
}