using LocalEmailExplorer.Domain.Entities.EmailEntities;
using LocalEmailExplorer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalEmailExplorer.Application.DTOs.EmailDtos;

namespace LocalEmailExplorer.Application.Services.Interfaces.EmailServiceInterfaces
{
    public interface IEmailService
    {
        Task<EmailDto> CreateEmailAsync(CreateEmailDto email);
        Task<bool> UpdateEmailAsync(int id, UpdateEmailDto email);
        Task<bool> DeleteEmailAsync(string emailAddress);
        Task<EmailDto> GetEmailByIdAsync(int id);
        Task<List<EmailDto>> GetEmailsByPhoneAsync(string phoneNumber);
        Task<List<EmailDto>> GetEmailsByRecoveryEmailAsync(string recoveryEmail);
        Task<List<EmailDto>> GetEmailsAsync();
        Task<bool> IsEmailExistsByEmailAddressAsync(string emailAddress);
        Task<bool> IsEmailExistsByIdAsync(int id);
        Task<EmailDto> GetEmailByEmailAddressAsync(string emailAddress, bool track);

        Task<PaginatedResult<EmailDto>> GetEmailsPagedAsync(int pageNumber, int pageSize);
    }
}
