using CleanArch26.Domain.Abstraction;
using CleanArch26.Domain.Employees;
using CleanArch26.Domain.Repository;
using CleanArch26.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CleanArch26.Infrastructure.Context;
public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<AppUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Entity>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(p => p.CreateAt)
                    .CurrentValue = DateTimeOffset.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                // soft delete
                if (entry.Property(p => p.IsDeleted).CurrentValue == true)
                {
                    entry.Property(p=> p.DeleteAt)
                        .CurrentValue = DateTimeOffset.Now;
                }
                else
                {
                    entry.Property(p => p.UpdateAt)
                     .CurrentValue = DateTimeOffset.Now;
                }

                if(entry.State == EntityState.Deleted)
                {
                    throw new ArgumentException("You cannot delete an entity. Use soft delete instead.");
                }
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

}

