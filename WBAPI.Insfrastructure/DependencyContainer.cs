using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WBAPI.Application.Interfaces;
using WBAPI.Domain.Interfaces;
using WBAPI.Infrastructure.Implementations.Common;
using WBAPI.Infrastructure.Implementations.Repositories;
using WBAPI.Insfrastructure.Implementations.Identity;
using WBAPI.Insfrastructure.Implementations.Persistence;
using WBAPI.Insfrastructure.Implementations.Services;
using WBAPI.Insfrastructure.Implementations.Settings;

namespace WBAPI.Insfrastructure
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddDbContext<AppDbContext>(opts =>
                opts.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .LogTo(Console.WriteLine, LogLevel.Information));
           
            services
                .AddIdentity<AppUser, IdentityRole>(opts =>
                {
                    opts.Password.RequireDigit = true;
                    opts.Password.RequiredLength = 8;
                    opts.Password.RequireUppercase = true;
                    opts.Password.RequireNonAlphanumeric = false;

                    opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                    opts.Lockout.MaxFailedAccessAttempts = 5;
                    opts.Lockout.AllowedForNewUsers = true;

                    opts.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<AppDbContext>()  
                .AddDefaultTokenProviders();        

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.Configure<IdentityRolesSettings>(configuration.GetSection("IdentityRoles"));
            
            services.AddScoped<IJwtTokenService, JwtTokenServiceImp>();
            services.AddScoped<IIdentityService, IdentityServiceImp>();
            services.AddScoped<IUnitOfWork, UnitOfWorkImp>();
            services.AddScoped<IAlbumRepository, AlbumRepository>();

            return services;
        }
    }
}
