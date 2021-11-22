using System;

namespace DoctorDiary.Shared.Domain
{
    public abstract class FullAuditedAggregateRoot<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; }
        public DateTime CreationTime { get; }

        protected FullAuditedAggregateRoot()
        {
            
        }
       
        public FullAuditedAggregateRoot(TKey id)
        {
            Id = id;
            CreationTime = DateTime.UtcNow;
        }
    }
}