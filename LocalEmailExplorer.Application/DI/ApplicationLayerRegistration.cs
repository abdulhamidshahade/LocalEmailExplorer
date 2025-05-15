using LocalEmailExplorer.Application.Services.Concretes.AuthServiceConcretes;
using LocalEmailExplorer.Application.Services.Concretes.EmailServiceConretes;
using LocalEmailExplorer.Application.Services.Interfaces.AuthServiceInterfaces;
using LocalEmailExplorer.Application.Services.Interfaces.EmailServiceInterfaces;
using Microsoft.Extensions.DependencyInjection;


namespace LocalEmailExplorer.Application.DI
{
    public static class ApplicationLayerRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IAuthService, AuthService>();
            serviceDescriptors.AddScoped<IRoleService, RoleService>();
            serviceDescriptors.AddScoped<ITokenService, TokenService>();
            serviceDescriptors.AddScoped<IUserService, UserService>();

            serviceDescriptors.AddScoped<IEmailService, EmailService>();

            serviceDescriptors.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return serviceDescriptors;
        }
    }
}
