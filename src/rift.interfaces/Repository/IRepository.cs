using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using rift.domain;

namespace rift.interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        // Get
        Task<T> FindByIdAsync(int id);
        Task<T> FindByIdAsync(int id, params string[] includes);
        Task<T> FindByAsync(Expression<Func<T, bool>> criteria);
        Task<T> FindByAsync(Expression<Func<T, bool>> criteria, params string[] includes);

        Task<IList<T>> FindManyAsync();
        Task<IList<T>> FindManyAsync(params string[] includes);
        Task<IList<T>> FindManyAsync(Expression<Func<T, bool>> criteria);
        Task<IList<T>> FindManyAsync(Expression<Func<T, bool>> criteria, params string[] includes);
        Task<IList<T>> FindManyAsync(string[] includes, params Expression<Func<T, bool>>[] criteria);

        // Add && Update
        Task<T> SaveAsync(T entity);
        Task<T> SaveAsync(T entity, params object[] attachments);
        Task<T> UpdateAsync(T entity);

        // Delete
        Task<T> DeleteAsync(T entity);
        Task<int> DeleteManyAsync(ICollection<T> entities);

        // Helper
        Task<bool> Exists(Expression<Func<T, bool>> criteria);
        void Detach(T entity);
    }
}
