using Framework.Application.Common.Interfaces;
using Framework.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext'in tanımını sağladık appsettings.json içerisinden connectionstringimize göre veritabanı ayarlarımızı tamamladık.

        services.AddDbContext<ApplicationDbContext>(options =>
         options
             .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                 builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
             .UseLazyLoadingProxies() // Proxies'i etkinleştir
             .EnableSensitiveDataLogging(), ServiceLifetime.Transient);


        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddScoped<ApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}
