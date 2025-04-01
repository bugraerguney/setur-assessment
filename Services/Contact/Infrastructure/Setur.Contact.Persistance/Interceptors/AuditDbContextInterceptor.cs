using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Setur.Contact.Domain.Entities.Common;

namespace Setur.Contact.Persistance.Interceptors
{
    public class AuditDbContextInterceptor : SaveChangesInterceptor
    {
        private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> _behaviors = new()
        {
            {EntityState.Added,AddBehavior },
            {EntityState.Modified,ModifiedBehavior}
        };

        private static void AddBehavior(DbContext context, IAuditEntity auditEntity)
        {

            auditEntity.Created = DateTime.UtcNow;
            context.Entry(auditEntity).Property(x => x.Updated).IsModified = false;
        }

        private static void ModifiedBehavior(DbContext context, IAuditEntity auditEntity)
        {
            context.Entry(auditEntity).Property(x => x.Created).IsModified = false;
            auditEntity.Updated = DateTime.UtcNow;
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {

            foreach (var item in eventData.Context!.ChangeTracker.Entries().ToList())
            {
                if (item.Entity is not IAuditEntity auditEntity) continue;

                if (item.State is not EntityState.Added and not EntityState.Modified)
                {
                    continue;
                }
                _behaviors[item.State](eventData.Context, auditEntity);

 


            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
