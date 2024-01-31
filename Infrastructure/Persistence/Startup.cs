using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Initialization;
using Infrastructure.Middleware;
using Application.Core.Products;
using Application.Core.Organizations;
using Application.Auth.Users;
using Application.Auth.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Core;

namespace Infrastructure.Persistence
{
    internal static class Startup
    {
        internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddDbContext<MasterDbContext>(options => options.UseSqlServer(config.GetConnectionString("MasterBas3Connection")))
                .AddDbContext<ProductDbContext>(options => options.UseSqlServer(config.GetConnectionString("BaseConnection")))
                .AddTransient<IDatabaseInitializer, DatabaseInitializer>()
                .AddTransient<ApplicationDbInitializer>()
                .AddTransient<ApplicationDbSeeder>()
                .AddTransient<IProductService, ProductService>()
                .AddTransient<IOrganizationService, OrganizationService>()
                .AddTransient<IUserService, MultiTenant.Infrastructure.Auth.UserService>()
                .AddTransient<ITokenService, MultiTenant.Infrastructure.Auth.TokenService>(); 
        }
    }
}
