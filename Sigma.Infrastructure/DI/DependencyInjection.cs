using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

using Sigma.Application.Interfaces;
using Sigma.Application.Interfaces.Common;
using Sigma.Application.Interfaces.Master;
using Sigma.Application.Interfaces.Services;
using Sigma.Application.Interfaces.Services.Common;
using Sigma.Application.Interfaces.Services.Master;
using Sigma.Application.Interfaces.Utilities;
using Sigma.Application.Services.Master;
using Sigma.Application.UseCases.Utilities;

using Sigma.Infrastructure.Persistence;
using Sigma.Infrastructure.Persistence.MongoDB;
using Sigma.Infrastructure.Repositories;
using Sigma.Infrastructure.Repositories.Common;
using Sigma.Infrastructure.Repositories.Interfaces;
using Sigma.Infrastructure.Repositories.Master;
using Sigma.Infrastructure.Repositories.Utilities;
using Sigma.Infrastructure.Security;
using Sigma.Infrastructure.Services;

namespace Sigma.Infrastructure.DI
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

            services.AddScoped<IAcademicYearRepository, AcademicYearRepository>();
            services.AddScoped<IMClassRepository, MClassRepository>();
            services.AddScoped<ISectionLookupRepository, SectionLookupRepository>();


            // services 
            services.AddScoped<IAcademicYearService, AcademicYearService>();
            services.AddScoped<IMClassService, MClassService>();
            services.AddScoped<ISectionLookupService, SectionLookupService>();

            

            // Security
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            // UseCases
            services.AddScoped<LoginUserUseCase>();

            // MongoDB (NO Options, NO Configure)
            services.AddSingleton<MongoDbContext>();
            services.AddSingleton<IMongoDatabase>(sp =>
                sp.GetRequiredService<MongoDbContext>().Database);

            // Global Activity Log
            services.AddSingleton<MongoSequenceService>();
            services.AddScoped<GlobalActivityLogRepository>();
            services.AddScoped<IGlobalActivityLogService, GlobalActivityLogService>();

            // Common Search
            services.AddScoped<ICommonSearchRepository, CommonSearchRepository>();
            services.AddScoped<ICommonSearchService, CommonSearchService>();
            return services;
        }
    }
}
