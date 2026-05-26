using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WBAPI.Insfrastructure.Implementations.Persistence.Seeders
{
    public static class RoleSeeder
    {
        private static readonly string[] Roles = ["Admin", "User", "Reader"];

        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("RoleSeeder");

            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    logger.LogInformation("Rol '{Role}' creado.", role);
                }
            }
        }
    }
}
