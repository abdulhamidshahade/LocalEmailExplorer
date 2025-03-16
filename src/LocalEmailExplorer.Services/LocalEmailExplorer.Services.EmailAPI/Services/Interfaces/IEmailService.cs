using LocalEmailExplorer.Services.EmailAPI.Models.DTOs;

namespace LocalEmailExplorer.Services.EmailAPI.Services.Interfaces
{
    public interface IEmailService
    {
        Task<Email> CreateEmailAsync(CreateEmailDto emailDto);
        Task<bool> UpdateEmailAsync(UpdateEmailDto emailDto, string id);
        Task<bool> DeleteEmailAsync(DeleteEmailDto emailDto);
        Task<Email> GetEmailByIdAsync(string id);
        Task<List<Email>> GetEmailsByPhoneAsync(string phoneNumber);
        Task<List<Email>> GetEmailsByRecoveryEmailAsync(string recoveryEmail);
        Task<List<Email>> GetEmailsAsync();
        Task<bool> IsEmailExistsByEmailAddressAsync(string emailAddress);
        Task<bool> IsEmailExistsByIdAsync(string id);
        Task<Email> GetEmailByEmailAddressAsync(string emailAddress, bool track);
    }
}
