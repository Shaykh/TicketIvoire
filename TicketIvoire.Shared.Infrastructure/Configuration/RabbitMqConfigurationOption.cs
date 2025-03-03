namespace TicketIvoire.Shared.Infrastructure.Configuration;

public class RabbitMqConfigurationOption
{
    public const string AppSettingsPathKey = "RabbitMq";
    
    public static readonly ushort DefaultRabbitMqPort = 5672;

    public string HostName { get; set; } = string.Empty;

    public string VirtualHost { get; set; } = string.Empty;

    public string User { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public ushort Port { get; set; } = DefaultRabbitMqPort;
}
