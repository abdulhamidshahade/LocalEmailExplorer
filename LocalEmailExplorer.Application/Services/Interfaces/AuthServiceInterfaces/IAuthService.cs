

using LocalEmailExplorer.Application.Dtos.AuthDtos;

namespace LocalEmailExplorer.Application.Services.Interfaces.AuthServiceInterfaces
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> Register(RegisterRequestDto requestDto);
        Task<LoginResponseDto> Login(LoginRequestDto requestDto);
    }
}
