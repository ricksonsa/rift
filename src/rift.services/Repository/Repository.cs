﻿using System;
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

        public Repository(AcervoContext context)
        {
            _context = context;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            var entry = _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<int> DeleteManyAsync(ICollection<T> entities)
        {

            _context.Set<T>().RemoveRange(entities);
            return await _context.SaveChangesAsync();
        }

        public async Task<T> FindByAsync(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> entityQuery = _context.Set<T>()
                 .AsNoTracking();

            return await entityQuery.Where(criteria).FirstOrDefaultAsync();
        }

        public async Task<IList<T>> FindManyAsync(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> entityQuery = _context.Set<T>()
                .AsNoTracking();

            return await entityQuery.Where(criteria).ToListAsync();
        }

        public async Task<T> FindByAsync(Expression<Func<T, bool>> criteria, params string[] includes)
        {
            IQueryable<T> entityQuery = _context.Set<T>()
                  .AsNoTracking();

            foreach (var include in includes)
            {
                entityQuery = entityQuery.Include(include);
            }

            return await entityQuery.Where(criteria).FirstOrDefaultAsync();
        }

        public async Task<IList<T>> FindManyAsync(Expression<Func<T, bool>> criteria, params string[] includes)
        {
            IQueryable<T> entityQuery = _context.Set<T>()
                 .AsNoTracking();

            foreach (var include in includes)
            {
                entityQuery = entityQuery.Include(include);
            }

            return await entityQuery.Where(criteria).ToListAsync();
        }

        public async Task<IList<T>> FindManyAsync(params string[] includes)
        {

            IQueryable<T> entityQuery = _context.Set<T>()
                 .AsNoTracking();

            foreach (var include in includes)
            {
                entityQuery = entityQuery.Include(include);
            }

            return await entityQuery.ToListAsync();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            IQueryable<T> entityQuery = _context.Set<T>()
                .AsNoTracking();

            return await entityQuery.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<T> FindByIdAsync(int id, params string[] includes)
        {
            IQueryable<T> entityQuery = _context.Set<T>()
                .AsNoTracking();

            foreach (var include in includes)
            {
                entityQuery = entityQuery.Include(include);
            }

            return await entityQuery.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IList<T>> FindManyAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }


        public async Task<T> SaveAsync(T entity)
        {

            EntityEntry<T> entry;
            if (entity.Id == 0)
            {
                entry = _context.Set<T>().Add(entity);
            }
            else
            {
                entry = _context.Set<T>().Update(entity);
            }
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public void Detach(T entity)
        {
            
            var local = _context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entity.Id));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }

        public async Task<T> SaveAsync(T entity, object[] attachments)
        {
            
            EntityEntry<T> entry;
            foreach (var attachment in attachments)
            {
                _context.Attach(attachment);
            }
            if (entity.Id == 0)
            {
                entry = _context.Set<T>().Add(entity);
            }
            else
            {
                entry = _context.Set<T>().Update(entity);
            }
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<T> FindByAsync(string[] includes, params Expression<Func<T, bool>>[] criterias)
        {
            IQueryable<T> entityQuery = _context.Set<T>()
               .AsNoTracking();

            foreach (var include in includes)
            {
                entityQuery = entityQuery.Include(include);
            }
            foreach (var criteria in criterias)
            {
                var result = await entityQuery.FirstOrDefaultAsync(criteria);
                if (result != null) return result;
            }
            return null;
        }
    }
}