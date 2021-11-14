using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorDiary.Shared;
using DoctorDiary.Shared.Domain;

namespace DoctorDiary.EntityFrameworkCore
{
    public interface IRepository<TEntity, in TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<TEntity> GetAsync(TKey key);

        Task<List<TEntity>> GetListAsync(int count, int skipCount, bool asNoTracking = false);

        Task<List<TEntity>> GetListAsync(Func<TEntity, bool> predicate, bool asNoTracking = false);

        Task<TEntity> InsertAsync(TEntity entity);
        
        Task<TEntity> UpdateAsync(TEntity entity);
        
        Task DeleteAsync(TKey key);
    }
}