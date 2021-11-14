using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorDiary.Shared;
using DoctorDiary.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace DoctorDiary.EntityFrameworkCore
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        protected IApplicationDbContext DbContext { get; }
        protected DbSet<TEntity> EntityDbSet { get;  }

        protected RepositoryBase()
        {
            DbContext = DependencyService.Get<IApplicationDbContext>();
            EntityDbSet = DbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetAsync(TKey key)
        {
            return await EntityDbSet.SingleAsync(e => e.Id.Equals(key));
        }

        public async Task<List<TEntity>> GetListAsync(int count, int skipCount, bool asNoTracking = false)
        {
            var query = EntityDbSet.Skip(skipCount).Take(count);

            if (asNoTracking)
            {
                query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> GetListAsync(Func<TEntity, bool> predicate, bool asNoTracking = false)
        {
            var query = EntityDbSet.Where(predicate).AsQueryable();
                
            if (asNoTracking)
            {
                query.AsNoTracking();
            }
            
            return await query.ToListAsync();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await EntityDbSet.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            EntityDbSet.Update(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(TKey key)
        {
            var entity = await GetAsync(key);
            EntityDbSet.Remove(entity);
            
            await DbContext.SaveChangesAsync();
        }
    }
}