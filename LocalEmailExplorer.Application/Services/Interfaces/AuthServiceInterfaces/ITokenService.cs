



using LocalEmailExplorer.Domain.Entities.AuthEntities;

namespace LocalEmailExplorer.Application.Services.Interfaces.AuthServiceInterfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}
