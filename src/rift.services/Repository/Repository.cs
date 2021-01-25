using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using rift.data;
using rift.domain;
using rift.interfaces.Repository;

namespace rift.services.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AcervoContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AcervoContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(entity.GetType().ToString());
            var entry = _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public virtual async Task<int> DeleteManyAsync(ICollection<T> entities)
        {
            _dbSet.RemoveRange(entities);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<T> FindByAsync(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> entityQuery = _dbSet.AsNoTracking();
            return await entityQuery.Where(criteria).FirstOrDefaultAsync();
        }

        public virtual async Task<IList<T>> FindManyAsync(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> entityQuery = _dbSet
                .AsNoTracking();

            return await entityQuery.Where(criteria).ToListAsync();
        }

        public virtual async Task<T> FindByAsync(Expression<Func<T, bool>> criteria, params string[] includes)
        {
            IQueryable<T> entityQuery = _dbSet
                  .AsNoTracking();

            foreach (var include in includes)
            {
                entityQuery = entityQuery.Include(include);
            }

            return await entityQuery.Where(criteria).FirstOrDefaultAsync();
        }

        public virtual async Task<IList<T>> FindManyAsync(Expression<Func<T, bool>> criteria, params string[] includes)
        {
            IQueryable<T> entityQuery = _dbSet
                 .AsNoTracking();

            foreach (var include in includes)
            {
                entityQuery = entityQuery.Include(include);
            }

            return await entityQuery.Where(criteria).ToListAsync();
        }

        public virtual async Task<IList<T>> FindManyAsync(params string[] includes)
        {

            IQueryable<T> entityQuery = _dbSet
                 .AsNoTracking();

            foreach (var include in includes)
            {
                entityQuery = entityQuery.Include(include);
            }

            return await entityQuery.ToListAsync();
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            IQueryable<T> entityQuery = _dbSet
                .AsNoTracking();

            return await entityQuery.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<T> FindByIdAsync(int id, params string[] includes)
        {
            IQueryable<T> entityQuery = _dbSet
                .AsNoTracking();

            foreach (var include in includes)
            {
                entityQuery = entityQuery.Include(include);
            }

            return await entityQuery.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<IList<T>> FindManyAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }


        public virtual async Task<T> SaveAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(entity.GetType().ToString());

            EntityEntry<T> entry;
            if (entity.Id == 0)
            {
                entry = _dbSet.Add(entity);
            }
            else
            {
                entry = _dbSet.Update(entity);
            }
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public void Detach(T entity)
        {
            if (entity == null) throw new ArgumentNullException(entity.GetType().ToString());

            var local = _dbSet
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entity.Id));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }

        public virtual async Task<T> SaveAsync(T entity, params object[] attachments)
        {
            if (entity == null) throw new ArgumentNullException(entity.GetType().ToString());

            EntityEntry<T> entry;
            foreach (var attachment in attachments)
            {
                _context.Attach(attachment);
            }
            if (entity.Id == 0)
            {
                entry = _dbSet.Add(entity);
            }
            else
            {
                entry = _dbSet.Update(entity);
            }
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(entity.GetType().ToString());
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<IList<T>> FindManyAsync(string[] includes, params Expression<Func<T, bool>>[] criterias)
        {
            IQueryable<T> entityQuery = _dbSet
               .AsNoTracking();

            foreach (var include in includes)
            {
                entityQuery = entityQuery.Include(include);
            }
            foreach (var criteria in criterias)
            {
                entityQuery = entityQuery.Where(criteria);
            }
            return await entityQuery.ToListAsync();
        }

        public virtual async Task<bool> Exists(Expression<Func<T, bool>> criteria)
        {
            return await _dbSet.AnyAsync(criteria);
        }
    }

}
