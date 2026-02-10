using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sigma.Application.Interfaces;
using Sigma.Application.Interfaces.Services;
using Sigma.Application.UseCases.Utilities;
using Sigma.Infrastructure.Persistence;
using Sigma.Infrastructure.Repositories;
using Sigma.Infrastructure.Repositories.Utilities;
using Sigma.Infrastructure.Security;

namespace Sigma.Infrastructure.DI   // ✅ FIXED
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // PostgreSQL / Dapper
            services.AddSingleton<DapperContext>();

            // Repositories
            services.AddScoped<IAuthUserRepository, AuthUserRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();


            // Security
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            // UseCases
            services.AddScoped<LoginUserUseCase>();



            return services;
        }
    }
}
