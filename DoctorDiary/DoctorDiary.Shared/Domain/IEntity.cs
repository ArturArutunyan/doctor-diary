using System;

namespace DoctorDiary.Shared.Domain
{
    public interface IEntity
    {
        
    } 
    
    public interface IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey Id { get; }
    }
}