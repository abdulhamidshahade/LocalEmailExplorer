



using LocalEmailExplorer.Domain.Entities.AuthEntities;

namespace LocalEmailExplorer.Application.Services.Interfaces.AuthServiceInterfaces
{
    public interface IRoleService
    {
        Task EnsureRoleExistsAsync(string roleName);
        Task<bool> AssignRoleToUserAsync(ApplicationUser user, string roleName);
        bool IsValidRole(string roleName);
    }
}
