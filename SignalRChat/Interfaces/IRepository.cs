using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace SignalRChat.Interfaces
{
    public interface IRepository<T>
       where T : class
    {
        Task<IList<T>> GetAsync(
             Expression<Func<T, bool>>? filter = null,
             Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
             string includeProperties = "",
             bool asNoTracking = true);

        public Task<IList<T>> QueryAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int? take = null, int skip = 0,
            bool asNoTracking = true);

        Task<T?> GetById(int id, string includeProperties = "");

        Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool asNoTracking = true);

        Task InsertAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task SaveChangesAsync();
    }
}
