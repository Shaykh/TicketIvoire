using Microsoft.Extensions.Configuration;
using TicketIvoire.Shared.Infrastructure.Configuration;
using TicketIvoire.Shared.Infrastructure.Messaging;

namespace TicketIvoire.Shared.Infrastructure.Tests.Messaging;
#pragma warning disable CA1707 
public class MassTransitConfiguratorTests
{
    [Fact]
    public void GetRabbitMqConfiguration_WhenCalled_ThenReturnsRabbitMqConfigurationOption()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection([
                new KeyValuePair<string, string?>($"{RabbitMqConfigurationOption.AppSettingsPathKey}:HostName", "localhost"),
                new KeyValuePair<string, string?>($"{RabbitMqConfigurationOption.AppSettingsPathKey}:VirtualHost", "/"),
                new KeyValuePair<string, string?>($"{RabbitMqConfigurationOption.AppSettingsPathKey}:User", "rabbitmq"),
                new KeyValuePair<string, string?>($"{RabbitMqConfigurationOption.AppSettingsPathKey}:Password", "rabbitmq"),
                new KeyValuePair<string, string?>($"{RabbitMqConfigurationOption.AppSettingsPathKey}:Port", "5672"),
                ])
            .Build();

        // Act
        RabbitMqConfigurationOption result = configuration.GetRabbitMqConfiguration();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("localhost", result.HostName);
        Assert.Equal("/", result.VirtualHost);
        Assert.Equal("rabbitmq", result.User);
        Assert.Equal("rabbitmq", result.Password);
        Assert.Equal(5672, result.Port);
    }
}
#pragma warning restore CA1707
