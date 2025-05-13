using LocalEmailExplorer.Domain.Entities;
using LocalEmailExplorer.Domain.Entities.EmailEntities;
using LocalEmailExplorer.Domain.Repositories.UserEmailInterfaces;
using LocalEmailExplorer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LocalEmailExplorer.Infrastructure.Repositories.UserEmailConcretes
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmailRepository> _logger;

        public EmailRepository(ApplicationDbContext context, 
                                   IUnitOfWork unitOfWork, 
                                   ILogger<EmailRepository> logger)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Email> CreateEmailAsync(Email email)
        {
            await _context.Emails.AddAsync(email);
            
            if(await _unitOfWork.SaveChangesAsync())
            {
                return email;
            }
            return null;
        }

        public async Task<bool> DeleteEmailAsync(string emailAddress)
        {
            var email = await _context.Emails
                .Where(e => e.EmailAddress == emailAddress)
                .FirstOrDefaultAsync();

            _context.Emails.Remove(email);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Email> GetEmailByEmailAddressAsync(string emailAddress, bool track)
        {
            IQueryable<Email> query = _context.Emails;

            if (!track)
            {
                query = query.AsNoTracking();
            }

            var email = await query.FirstOrDefaultAsync(e => e.EmailAddress == emailAddress);

            return email;
        }

        public async Task<Email> GetEmailByIdAsync(int Id)
        {
            var email = await _context.Emails.FirstOrDefaultAsync(e => e.Id == Id);

            return email;
        }

        public async Task<List<Email>> GetEmailsAsync()
        {
            var emails = await _context.Emails.ToListAsync();

            return emails ?? new List<Email>();
        }

        public async Task<List<Email>> GetEmailsByPhoneAsync(string phoneNumber)
        {
            var emails = await _context.Emails.Where(p => p.Phone == phoneNumber).ToListAsync();

            return emails ?? new List<Email>();
        }

        public async Task<List<Email>> GetEmailsByRecoveryEmailAsync(string recoveryEmail)
        {
            var emails = await _context.Emails.Where(r => r.RecoveryEmail == recoveryEmail).ToListAsync();
            return emails ?? new List<Email>();
        }

        public async Task<PaginatedResult<Email>> GetEmailsPagedAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Emails.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = await _context.Emails
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Email>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public async Task<bool> IsEmailExistsByEmailAddressAsync(string emailAddress)
        {
            return await _context.Emails.AnyAsync(e => e.EmailAddress == emailAddress);
        }

        public async Task<bool> IsEmailExistsByIdAsync(int id)
        {
            return await _context.Emails.AnyAsync(i => i.Id == id);
        }

        public async Task<bool> UpdateEmailAsync(int id, Email email)
        {

            var existingEmail = await _context.Emails.FirstOrDefaultAsync(i => i.Id == id);

            if(email == null)
            {
                return false;
            }

            email.UpdatedAt = DateTime.UtcNow;
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
