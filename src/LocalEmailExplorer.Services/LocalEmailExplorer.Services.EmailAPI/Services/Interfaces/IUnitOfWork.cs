using LocalEmailExplorer.Services.EmailAPI.Data;

namespace LocalEmailExplorer.Services.EmailAPI.Services.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
