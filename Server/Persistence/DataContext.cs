﻿using Microsoft.EntityFrameworkCore;
using Server.Common.Infrastructure;
using Server.Core.Models;

namespace Server.Persistence;

internal sealed class DataContext : BaseDbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("identity");

        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<IdentityUser> Users { get; set; } = default!;
}