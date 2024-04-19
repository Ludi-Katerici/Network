using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Server.Common.Core;
using Server.Common.Core.Abstract;

namespace Server.Common.Infrastructure;

public static class DbContextExtensionMethods
{
    public static void ApplyAuditInfo(this ChangeTracker changeTracker, IClock clock)
    {
        var changedEntries = changeTracker
            .Entries()
            .Where(e =>
                e is
                {
                    Entity: IAuditInformation,
                    State: EntityState.Added or EntityState.Modified
                });

        foreach (var entry in changedEntries)
        {
            var entity = (IAuditInformation)entry.Entity;
            if (entry.State == EntityState.Added && entity.CreatedOn == default)
            {
                entity.CreatedOn = clock.CurrentDateTime();
            }
            else
            {
                entity.ModifiedOn = clock.CurrentDateTime();
            }
        }
    }
}