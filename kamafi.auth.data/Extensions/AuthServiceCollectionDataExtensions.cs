using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace kamafi.auth.data.extensions
{
    public static class AuthServiceCollectionDataExtensions
    {
        public static IServiceCollection AddDataConfiguration(this IServiceCollection services, IConfiguration config)
        {
            var configurationSection = config.GetDbSection();
            var options = configurationSection.Get<AuthDbOptions>();

            services.AddDbContext<AuthDbContext>(o => 
                o.UseLazyLoadingProxies()
                    .UseNpgsql(options.ConnectionString,
                    no =>
                    {
                        no.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        no.MigrationsAssembly("kamafi.auth.data.migrations");
                    }));

            return services;
        }
    }
}
