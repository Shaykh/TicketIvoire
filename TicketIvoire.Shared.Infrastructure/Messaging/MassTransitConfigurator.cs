using MassTransit;
using MassTransit.Transports.Fabric;
using Microsoft.Extensions.Configuration;
using TicketIvoire.Shared.Infrastructure.Configuration;

namespace TicketIvoire.Shared.Infrastructure.Messaging;

public static class MassTransitConfigurator
{
    public const string ReceiveAllEvents = "#";

    public static RabbitMqConfigurationOption GetRabbitMqConfiguration(this IConfiguration configuration)
        => configuration.GetConfigOption<RabbitMqConfigurationOption>(nameof(RabbitMqConfigurationOption));

    public static void ConfigureMassTransitConsumer<TConsumer, TMessage>(this IRabbitMqBusFactoryConfigurator rabbitMqBusFactoryConfigurator, 
        IBusRegistrationContext busRegistrationContext, 
        params string[] routingKeys)
        where TConsumer : class, IConsumer where TMessage : class
    {
        if (routingKeys == null || routingKeys.Length == 0)
        {
            throw new ArgumentNullException(nameof(routingKeys));
        }

        rabbitMqBusFactoryConfigurator.ReceiveEndpoint(typeof(TConsumer).FullName ?? "Error", conf =>
        {
            conf.ConfigureConsumeTopology = false;
            conf.PublishFaults = true;
            conf.ConfigureConsumer<TConsumer>(busRegistrationContext);
            foreach (string routingKey in routingKeys)
            {
                conf.Bind<TMessage>(bindConf => bindConf.RoutingKey = routingKey);
            }
        });
    }

    public static void ConfigureTopicFor<TMessage>(IRabbitMqBusFactoryConfigurator rmqConfiguration, 
        Func<TMessage, Guid> correlationIdProperty,
        Func<SendContext<TMessage>, string>? routingKeyFormatter = null) 
        where TMessage : class
    {
        routingKeyFormatter ??= (_ => typeof(TMessage).FullName!);
        rmqConfiguration.Send<TMessage>(sendConf =>
        {
            sendConf.UseCorrelationId(correlationIdProperty);
            sendConf.UseRoutingKeyFormatter(routingKeyFormatter);
        });
        rmqConfiguration.Publish<TMessage>(publishConf =>
        {
            publishConf.ExchangeType = ExchangeType.Topic.ToString().ToUpperInvariant();
            publishConf.AutoDelete = false;
            publishConf.BindAlternateExchangeQueue($"{typeof(TMessage).FullName!}-NotDelivered");
        });
    }
}
