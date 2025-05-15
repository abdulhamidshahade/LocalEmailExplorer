namespace LocalEmailExplorer.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
    }
}
