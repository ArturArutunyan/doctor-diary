using System;
using System.Collections.Generic;
using DoctorDiary.Shared.Domain;

namespace DoctorDiary.Models.SickLeaves.ValueObjects
{
    public class Term : ValueObject
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public int Days => (EndDate - StartDate).Days;

        protected Term()
        {
        }

        protected Term(DateTime startDate, DateTime endDate)
        {
            // TODO: add validation
            StartDate = startDate;
            EndDate = endDate;
        }

        public static Term Create(DateTime startDate, DateTime endDate)
        {
            return new Term(startDate: startDate, endDate: endDate);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return StartDate;
            yield return EndDate;
        }
    }
}