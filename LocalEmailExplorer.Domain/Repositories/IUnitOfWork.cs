namespace LocalEmailExplorer.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
    }
}
