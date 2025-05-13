
using LocalEmailExplorer.Domain.Entities;
using LocalEmailExplorer.Domain.Entities.EmailEntities;

namespace LocalEmailExplorer.Domain.Repositories.UserEmailInterfaces
{
    public interface IEmailRepository
    {
        Task<Email> CreateEmailAsync(Email email);
        Task<bool> UpdateEmailAsync(int id, Email email);
        Task<bool> DeleteEmailAsync(string emailAddress);
        Task<Email> GetEmailByIdAsync(int id);
        Task<List<Email>> GetEmailsByPhoneAsync(string phoneNumber);
        Task<List<Email>> GetEmailsByRecoveryEmailAsync(string recoveryEmail);
        Task<List<Email>> GetEmailsAsync();
        Task<bool> IsEmailExistsByEmailAddressAsync(string emailAddress);
        Task<bool> IsEmailExistsByIdAsync(int id);
        Task<Email> GetEmailByEmailAddressAsync(string emailAddress, bool track);

        Task<PaginatedResult<Email>> GetEmailsPagedAsync(int pageNumber, int pageSize);
    }
}
