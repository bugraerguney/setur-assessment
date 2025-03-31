using Setur.Report.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Application.Contracts.Persistance
{
    public interface IGenericRepository<T,TId> where T : class  where TId : struct
    {
        Task<List<T>> GetAllAsync();
        ValueTask<T?> GetByIdAsync(Guid id);
        ValueTask AddAsync(T entity);

        void Update(T entity);

    }
}
