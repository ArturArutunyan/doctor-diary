using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DoctorDiary.Shared.Domain;

namespace DoctorDiary.EntityFrameworkCore
{
    public interface IRepository<TEntity, in TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<TEntity> GetAsync(TKey key);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        
        Task<List<TEntity>> GetListAsync(int takeCount, int skipCount, bool asNoTracking = false);

        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false);

        Task<TEntity> InsertAsync(TEntity entity);
        
        Task<TEntity> UpdateAsync(TEntity entity);
        
        Task DeleteAsync(TKey key);
    }
}