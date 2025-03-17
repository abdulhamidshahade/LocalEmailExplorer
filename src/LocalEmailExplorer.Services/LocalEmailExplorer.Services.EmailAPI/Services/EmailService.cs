using AutoMapper;
using LocalEmailExplorer.Services.EmailAPI.Data;
using LocalEmailExplorer.Services.EmailAPI.Halpers.Exceptions;
using LocalEmailExplorer.Services.EmailAPI.Models.DTOs;
using LocalEmailExplorer.Services.EmailAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace LocalEmailExplorer.Services.EmailAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmailService> _logger;

        public EmailService(ApplicationDbContext context, IMapper mapper, IUnitOfWork unitOfWork, ILogger<EmailService> logger)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Email> CreateEmailAsync(CreateEmailDto emailDto)
        {
            if(emailDto == null)
            {
                throw new ArgumentException(nameof(emailDto), "emailDto is null");
            }

            var email = _mapper.Map<Email>(emailDto);

            try
            {
                await _context.AddAsync(email);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Created email with ID: {EmailId}", email.Id);

                return email;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating email");
                throw;
            }
        }

        public async Task<bool> DeleteEmailAsync(DeleteEmailDto emailDto)
        {
            if (emailDto == null)
            {
                throw new ArgumentNullException(nameof(emailDto), "emailDto is null");
            }

            var email = await _context.Emails.FirstOrDefaultAsync(i => i.Id == emailDto.Id);

            if(email == null)
            {
                throw new EmailNotFoundException($"Email with ID {emailDto.Id} not found.");
            }

            _context.Emails.Remove(email);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<Email> GetEmailByEmailAddressAsync(string emailAddress, bool track)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentNullException(nameof(emailAddress), "Email address cannot be null or empty.");
            }

            IQueryable<Email> query = _context.Emails;

            if (!track)
            {
                query = query.AsNoTracking();
            }

            var email = await query.FirstOrDefaultAsync(e => e.EmailAddress == emailAddress);

            return email;
        }

        public async Task<Email> GetEmailByIdAsync(string Id)
        {
            if(string.IsNullOrEmpty(Id))
            {
                throw new ArgumentNullException(nameof(Id), "Id is null or empty");
            }

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
            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentNullException(nameof(phoneNumber), "Phone number is null or empty");
            }

            var emails = await _context.Emails.Where(p => p.Phone == phoneNumber).ToListAsync();

            return emails ?? new List<Email>();
        }

        public async Task<List<Email>> GetEmailsByRecoveryEmailAsync(string recoveryEmail)
        {
            if (string.IsNullOrEmpty(recoveryEmail))
            {
                throw new ArgumentNullException(nameof(recoveryEmail), "Recovery email is null");
            }

            var emails = await _context.Emails.Where(r => r.RecoveryEmail == recoveryEmail).ToListAsync();


            return emails ?? new List<Email>();
        }

        public async Task<bool> IsEmailExistsByEmailAddressAsync(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentNullException(nameof(emailAddress), "emailAddress is null");
            }

            return await _context.Emails.AnyAsync(e => e.EmailAddress == emailAddress);

        }

        public async Task<bool> IsEmailExistsByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "Id is null");
            }

            return await _context.Emails.AnyAsync(i => i.Id == id);
        }

        public async Task<bool> UpdateEmailAsync(UpdateEmailDto emailDto, string id)
        {
            if (string.IsNullOrEmpty(id) || emailDto == null)
            {
                throw new ArgumentNullException("Id or emailDto is null or empty");
            }

            var email = await _context.Emails.FirstOrDefaultAsync(i => i.Id == id);

            if(email == null)
            {
                throw new EmailNotFoundException($"Email with id '{id}' not found");
            }

            _mapper.Map(emailDto, email);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
