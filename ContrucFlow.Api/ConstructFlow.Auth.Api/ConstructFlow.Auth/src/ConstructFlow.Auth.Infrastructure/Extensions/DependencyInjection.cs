using ConstructFlow.Auth.Infrastructure.Persistance;
using ConstructFlow.Auth.Infrastructure.Persistance.Repositories;
using ConstructFlow.Auth.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infra.Extensions;

namespace ConstructFlow.Auth.Infrastructure.Extensions;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {


        private IServiceCollection AddDatabaseConfiguration(IConfiguration configuration)
        {
            services.AddDatabase<ApplicationDbContext>(configuration);
            return services;
        }

        private IServiceCollection AddRepositories()
        {
            services
                .AddRepository<Account, AccountRepository, IAccountRepository, ApplicationDbContext>()
                .AddRepository<RefreshToken, RefreshTokenRepository, IRefreshTokenRepository, ApplicationDbContext>()
                .AddRepos<ITokenService, TokenService>()
                .AddRepos<IPasswordService, PasswordService>()
                .AddRepos<IUnitOfWork, UnitOfWork>();

            return services;
        }


        public IServiceCollection AddInfrastructure(IConfiguration configuration)
        {
            return services
                .AddDatabaseConfiguration(configuration)
                .AddRepositories();
        }
    }
}