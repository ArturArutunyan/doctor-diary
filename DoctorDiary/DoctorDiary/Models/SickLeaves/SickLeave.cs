using System;
using System.Collections.Generic;
using System.Linq;
using DoctorDiary.Models.PatientCards;
using DoctorDiary.Models.SickLeaves.ValueObjects;
using DoctorDiary.Shared.Domain;
using DoctorDiary.Shared.SickLeaves;

namespace DoctorDiary.Models.SickLeaves
{
    public class SickLeave : FullAuditedAggregateRoot<Guid>
    {
        public virtual long Number { get; protected set; }
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
            Guid patientCardId,
            Term term) : base(id)
        {
            Number = number;
            PatientCardId = patientCardId;
            IsActive = true;
            
            _terms.Add(term);
        }
        
        public SickLeave(
            Guid id,
            long number,
            PatientCard patientCard,
            Term term) : this(id, number, patientCard.Id, term)
        {
           
        }

        public void ChangeNumber(long number)
        {
            // TODO: Add number validation. Should be 16? digits number.
            Number = number;
        }

        public void ChangeTerms(IEnumerable<Term> terms)
        {
            _terms.Clear();
            _terms.AddRange(terms);
        }

        public void ExtendSickLeave(Term term)
        {
            if (Terms.Count == SickLeaveConsts.DefaultMaxExtendCount)
            {
                throw new InvalidOperationException("Больничный не может быть продлен более трех раз");
            }
            
            _terms.Add(term);
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

        public int TotalOfDaysOnSickLeave()
        {
            return Terms.Sum(x => x.Days);
        }

        public int TotalDaysOnLastTerm()
        {
            return Terms.Last().Days;
        }
    }
}