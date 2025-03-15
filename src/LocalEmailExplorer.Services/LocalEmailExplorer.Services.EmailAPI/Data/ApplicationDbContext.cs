using LocalEmailExplorer.Services.EmailAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LocalEmailExplorer.Services.EmailAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Email> Emails { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
