// using DataAccess.Repositories;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DMT.DataAccess;

public static class Bootstrapper
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        // For Identity
        services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<UsersDbContext>()
                .AddDefaultTokenProviders();

        return services
            .AddDbContext<UsersDbContext>(options => options.UseNpgsql(connectionString));
    }
}