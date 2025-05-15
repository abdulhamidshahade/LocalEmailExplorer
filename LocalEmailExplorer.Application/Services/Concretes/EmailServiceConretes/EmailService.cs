using AutoMapper;
using LocalEmailExplorer.Application.DTOs.EmailDtos;
using LocalEmailExplorer.Application.Services.Interfaces.EmailServiceInterfaces;
using LocalEmailExplorer.Domain.Entities;
using LocalEmailExplorer.Domain.Entities.EmailEntities;
using LocalEmailExplorer.Domain.Repositories.UserEmailInterfaces;

namespace LocalEmailExplorer.Application.Services.Concretes.EmailServiceConretes
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _emailRepository;
        private readonly IMapper _mapper;

        public EmailService(IEmailRepository emailRepository,
                            IMapper mapper)
        {
            _emailRepository = emailRepository;
            _mapper = mapper;
        }

        public async Task<EmailDto> CreateEmailAsync(CreateEmailDto email)
        {
            if(email == null)
            {
                return null;
            }

            var mappedEmail = _mapper.Map<Email>(email);
            var createdEmail = await _emailRepository.CreateEmailAsync(mappedEmail);

            return _mapper.Map<EmailDto>(createdEmail);
        }

        public async Task<bool> DeleteEmailAsync(string emailAddress)
        {
            if(string.IsNullOrEmpty(emailAddress))
            {
                return false;
            }

            var deleteEmail = await _emailRepository.DeleteEmailAsync(emailAddress);

            return deleteEmail;
        }

        public async Task<EmailDto> GetEmailByEmailAddressAsync(string emailAddress, bool track)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                return null;
            }

            var email = await _emailRepository.GetEmailByEmailAddressAsync(emailAddress, track);
            return _mapper.Map<EmailDto>(email);
        }

        public async Task<EmailDto> GetEmailByIdAsync(int id)
        {
            if(id <= 0)
            {
                return null;
            }

            var email = await _emailRepository.GetEmailByIdAsync(id);
            return _mapper.Map<EmailDto>(email);
        }

        public async Task<List<EmailDto>> GetEmailsAsync()
        {
            var emails = await _emailRepository.GetEmailsAsync();
            return _mapper.Map<List<EmailDto>>(emails);
        }

        public async Task<List<EmailDto>> GetEmailsByPhoneAsync(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return null;
            }

            var emails = await _emailRepository.GetEmailsByPhoneAsync(phoneNumber);

            if(emails == null)
            {
                return null;
            }

            return _mapper.Map<List<EmailDto>>(emails);
        }

        public async Task<List<EmailDto>> GetEmailsByRecoveryEmailAsync(string recoveryEmail)
        {
            if (string.IsNullOrEmpty(recoveryEmail))
            {
                return null;
            }

            var emails = await _emailRepository.GetEmailsByRecoveryEmailAsync(recoveryEmail);

            if(emails == null)
            {
                return null;
            }

            return _mapper.Map<List<EmailDto>>(emails);
        }

        public async Task<PaginatedResult<EmailDto>> GetEmailsPagedAsync(int pageNumber, int pageSize)
        {
            if(pageNumber <= 0 || pageSize <= 0)
            {
                return null;
            }

            var pagedEmails = await _emailRepository.GetEmailsPagedAsync(pageNumber, pageSize);

            if(pagedEmails == null)
            {
                return null;
            }

            return _mapper.Map<PaginatedResult<EmailDto>>(pagedEmails);
        }

        public async Task<bool> IsEmailExistsByEmailAddressAsync(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                return false;
            }

            var isEmailExists = await _emailRepository.IsEmailExistsByEmailAddressAsync(emailAddress);
            return isEmailExists;
        }

        public async Task<bool> IsEmailExistsByIdAsync(int id)
        {
            if(id <= 0)
            {
                return false;
            }

            var isEmailExists = await _emailRepository.IsEmailExistsByIdAsync(id);
            return isEmailExists;
        }

        public async Task<bool> UpdateEmailAsync(int id, UpdateEmailDto email)
        {
            if(id <= 0 || email == null)
            {
                return false;
            }

            var mappedEmail = _mapper.Map<Email>(email);
            var updatedEmail = await _emailRepository.UpdateEmailAsync(id, mappedEmail);

            return updatedEmail;
        }
    }
}
