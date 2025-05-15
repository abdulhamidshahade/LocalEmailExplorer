

using LocalEmailExplorer.Domain.Repositories;
using LocalEmailExplorer.Domain.Repositories.UserEmailInterfaces;
using LocalEmailExplorer.Infrastructure.Data;
using LocalEmailExplorer.Infrastructure.Repositories;
using LocalEmailExplorer.Infrastructure.Repositories.UserEmailConcretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalEmailExplorer.Infrastructure.DI
{ 
    public static class InfrastructureLayerRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceDescriptors)
        {

            serviceDescriptors.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("MSSQLServer");

                options.UseSqlServer(connectionString,
                    sqloptions => sqloptions.MigrationsAssembly("BlueBerry24.Infrastructure"));
            });

            serviceDescriptors.AddScoped<IEmailRepository, EmailRepository>();
           
            serviceDescriptors.AddScoped<IUnitOfWork, UnitOfWork>();

           



            return serviceDescriptors;
        }
    }
}
