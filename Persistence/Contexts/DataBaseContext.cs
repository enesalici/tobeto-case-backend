using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Base.Entities.Auditing;

namespace Persistence.Contexts;

public class DataBaseContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<User> Users { get; set; }
    public DbSet<Domain.Entities.Task> Tasks { get; set; }

    public DataBaseContext(DbContextOptions<DataBaseContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<AuditableEntity<Guid>>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = DateOnly.FromDateTime(DateTime.UtcNow);
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedDate = DateOnly.FromDateTime(DateTime.UtcNow);
            }
            else if (entry.State == EntityState.Deleted)
            {
                entry.Entity.DeletedDate = DateOnly.FromDateTime(DateTime.UtcNow);
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
