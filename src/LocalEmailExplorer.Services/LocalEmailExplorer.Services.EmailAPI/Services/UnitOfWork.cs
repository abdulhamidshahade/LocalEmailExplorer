using LocalEmailExplorer.Services.EmailAPI.Data;
using LocalEmailExplorer.Services.EmailAPI.Services.Interfaces;

namespace LocalEmailExplorer.Services.EmailAPI.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
