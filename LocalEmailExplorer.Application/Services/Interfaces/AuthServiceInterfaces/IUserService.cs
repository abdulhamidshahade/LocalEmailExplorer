namespace LocalEmailExplorer.Application.Services.Interfaces.AuthServiceInterfaces
{
    public interface IUserService
    {
        Task<bool> IsUserExistsByIdAsync(int userId);
        Task<bool> IsUserExistsByEmailAsync(string emailAddress);
    }
}
