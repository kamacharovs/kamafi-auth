using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace kamafi.auth.data.extensions
{
    public static class AuthConfigurationExtensions
    {
        public static IConfigurationSection GetDbSection(this IConfiguration configuration)
        {
            var configurationSection = configuration.GetSection(AuthDbOptions.Section);

            if (configurationSection is null)
            {
                throw new ConfigurationErrorsException($"Configuration {AuthDbOptions.Section} is required");
            }

            return configurationSection;
        }
    }
}
