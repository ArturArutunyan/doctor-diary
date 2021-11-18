using System;
using System.Collections.Generic;
using System.Linq;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using DoctorDiary.Shared.Domain;

namespace DoctorDiary.Models.SickLeaves
{
    public class SickLeave : IEntity<Guid>
    {
        private const int DefaultMaxExtendCount = 3;
        public Guid Id { get; }
        public virtual long Number { get; }
        public virtual Guid PatientCardId { get; protected set; }
        public virtual bool IsActive { get; protected set; }
        
        private readonly List<Term> _terms = new List<Term>();
        public virtual IReadOnlyList<Term> Terms => _terms.OrderBy(t => t.StartDate).ToList().AsReadOnly();

        protected SickLeave()
        {
        }

        public SickLeave(
            Guid id,
            long number,
            PatientCard patientCard,
            Term term)
        {
            Id = id;
            Number = number;
            PatientCardId = patientCard.Id;
            IsActive = true;
            
            _terms.Add(term);
        }

        public void ChangeTerms(IEnumerable<Term> terms)
        {
            _terms.Clear();
            _terms.AddRange(terms);
        }

        public void ExtendSickLeave(Term term)
        {
            if (Terms.Count < DefaultMaxExtendCount)
            {
                _terms.Add(term);
            }
        }

        public void Close()
        {
            IsActive = false;
        }

        public DateTime LastTermEndDate()
        {
            return Terms.Last().EndDate;
        }

        public Term LastTerm()
        {
            return Terms.Last();
        }
    }
}