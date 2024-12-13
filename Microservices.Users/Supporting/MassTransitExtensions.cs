using Confluent.Kafka;
using MassTransit;
using Microservices.Core.Messages.Languages;
using Microservices.Core.Messages.Users;
using Microservices.Users.Application.Consumers;

namespace Microservices.Users.Supporting;

public static class MassTransitExtensions
{
    public static void AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(config =>
        {
            config.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });

            config.AddRider(x =>
            {
                x.AddProducer<OnLanguageUserAttachedMessage>("users.language-attached");
        
                x.UsingKafka((context, k) =>
                {
                    k.Host(configuration.GetSection("MassTransit")["KafkaEndpoint"]);
                    k.SecurityProtocol = SecurityProtocol.Plaintext;

                    k.TopicEndpoint<OnLanguageCreatedMessage>("language.created", "object-service", e =>
                    {
                        e.ConfigureConsumer<OnLanguageCreatedConsumer>(context);
                        e.CreateIfMissing();
                    });
                });

                x.AddConsumer<OnLanguageCreatedConsumer>();
            });
        });

    }
}