using System;
using DoctorDiary.EntityFrameworkCore.PatientCards;
using DoctorDiary.Models;
using DoctorDiary.Models.PatientCards;

namespace DoctorDiary.EntityFrameworkCore.PatientCards
{
    public class PatientCardEfCoreRepository : RepositoryBase<PatientCard, Guid>, IPatientCardRepository
    {
    }
}