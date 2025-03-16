using LocalEmailExplorer.Services.EmailAPI.Models.DTOs;

namespace LocalEmailExplorer.Services.EmailAPI.Services.Interfaces
{
    public interface IEmailService
    {
        Task<Email> CreateEmailAsync(EmailDto emailDto);
        Task<bool> UpdateEmailAsync(EmailDto emailDto, string id);
        Task<bool> DeleteEmailAsync(EmailDto emailDto);
        Task<Email> GetEmailByIdAsync(string id);
        Task<List<Email>> GetEmailsByPhoneAsync(string phoneNumber);
        Task<List<Email>> GetEmailsByRecoveryEmailAsync(string recoveryEmail);
        Task<List<Email>> GetEmailsAsync();
        Task<bool> IsEmailExistsByEmailAddressAsync(string emailAddress);
        Task<bool> IsEmailExistsByIdAsync(string id);
    }
}
