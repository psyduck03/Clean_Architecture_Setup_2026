namespace CleanArch26.Domain.Repository;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}