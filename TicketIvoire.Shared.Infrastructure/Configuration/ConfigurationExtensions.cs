using Microsoft.Extensions.Configuration;

namespace TicketIvoire.Shared.Infrastructure.Configuration;

public static class ConfigurationExtensions
{
    public static TConfigOption GetConfigOption<TConfigOption>(this IConfiguration configuration, string sectionName) where TConfigOption : new()
    {
        IConfigurationSection section = configuration.GetSection(sectionName);
        var configOption = new TConfigOption();
        section.Bind(configOption);

        return configOption;
    }
}
