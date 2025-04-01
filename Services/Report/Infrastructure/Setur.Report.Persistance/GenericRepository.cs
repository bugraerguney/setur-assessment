using Microsoft.EntityFrameworkCore;
using Setur.Report.Application.Contracts.Persistance;
using Setur.Report.Domain.Entities.Common;
using Setur.Report.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.Persistance
{
    public class GenericRepository<T,TId>(ReportDbContext context) : IGenericRepository<T, TId> where T : BaseEntity<TId> where TId : struct
    {
        protected ReportDbContext Context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();
        public Task<List<T>> GetAllAsync()
        {
            return _dbSet.AsNoTracking().ToListAsync();
        }

        public async ValueTask<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async ValueTask AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
