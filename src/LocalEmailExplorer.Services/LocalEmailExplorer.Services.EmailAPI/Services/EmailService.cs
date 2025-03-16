using AutoMapper;
using LocalEmailExplorer.Services.EmailAPI.Data;
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

        public EmailService(ApplicationDbContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Email> CreateEmailAsync(CreateEmailDto emailDto)
        {
            if(emailDto == null)
            {
                throw new Exception("emailDto is null");
            }

            var email = _mapper.Map<Email>(emailDto);

            await _context.AddAsync(email);
            await _unitOfWork.SaveChangesAsync();

            return email;
        }

        public async Task<bool> DeleteEmailAsync(DeleteEmailDto emailDto)
        {
            if (emailDto == null)
            {
                throw new Exception("emailDto is null");
            }

            var email = _mapper.Map<Email>(emailDto);

            _context.Remove(email);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<Email> GetEmailByIdAsync(string Id)
        {
            if(string.IsNullOrEmpty(Id))
            {
                throw new Exception("Id is null or empty");
            }

            var email = await _context.Emails.FirstOrDefaultAsync(e => e.Id == Id);

            if(email == null)
            {
                throw new Exception("email is null");
            }

            return email;
        }

        public async Task<List<Email>> GetEmailsAsync()
        {
            var emails = await _context.Emails.ToListAsync();

            return emails != null ? emails : throw new Exception("Emails is null");
        }

        public async Task<List<Email>> GetEmailsByPhoneAsync(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new Exception("Id is null or empty");
            }

            var emails = await _context.Emails.Where(p => p.Phone == phoneNumber).ToListAsync();

            return emails != null ? emails : throw new Exception("Email is null");
        }

        public async Task<List<Email>> GetEmailsByRecoveryEmailAsync(string recoveryEmail)
        {
            if (string.IsNullOrEmpty(recoveryEmail))
            {
                throw new Exception("recovery email is null");
            }

            var emails = await _context.Emails.Where(r => r.RecoveryEmail == recoveryEmail).ToListAsync();


            return emails != null ? emails : throw new Exception("Emails is null");
        }

        public async Task<bool> IsEmailExistsByEmailAddressAsync(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new Exception("emailAddress is null");
            }

            return await _context.Emails.AnyAsync(e => e.EmailAddress == emailAddress);

        }

        public async Task<bool> IsEmailExistsByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("Id is null");
            }

            return await _context.Emails.AnyAsync(i => i.Id == id);
        }

        public async Task<bool> UpdateEmailAsync(UpdateEmailDto emailDto, string id)
        {
            if (string.IsNullOrEmpty(id) || emailDto == null)
            {
                throw new Exception("Id or emailDto is null or empty");
            }

            var email = await _context.Emails.FirstOrDefaultAsync(i => i.Id == id);

            if(email == null)
            {
                throw new Exception("email is null");
            }

            email.Phone = emailDto.Phone;
            email.EmailAddress = emailDto.EmailAddress;
            email.RecoveryEmail = emailDto.RecoveryEmail;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
