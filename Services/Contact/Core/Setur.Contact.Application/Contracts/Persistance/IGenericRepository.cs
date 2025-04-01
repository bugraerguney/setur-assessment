using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Application.Contracts.Persistance
{
    public interface IGenericRepository<T, TId> where T : class where TId : struct
    {
        Task<List<T>> GetAllAsync();

        Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize);

        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        ValueTask<T?> GetByIdAsync(Guid id);
        ValueTask AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> AnyAsync(TId id);
    }
}
