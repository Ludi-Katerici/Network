using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Server.Common.Core.Abstract;

namespace Server.Common.Infrastructure;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public override int SaveChanges() => this.SaveChanges(true);

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        var clock = this.GetService<IClock>();
        this.ChangeTracker.ApplyAuditInfo(clock);
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        this.SaveChangesAsync(true, cancellationToken);

    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        var clock = this.GetService<IClock>();
        this.ChangeTracker.ApplyAuditInfo(clock);
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    } 
}