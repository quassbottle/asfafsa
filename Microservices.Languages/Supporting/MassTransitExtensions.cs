using MassTransit;
using Microservices.Core.Messages.Languages;
using Microservices.Core.Messages.Users;
using Microservices.Languages.Application.Consumers;

namespace Microservices.Languages.Supporting;

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
                x.AddProducer<OnLanguageCreatedMessage>("language.created");
        
                x.UsingKafka((context, k) =>
                {
                    k.Host(configuration.GetSection("MassTransit")["KafkaEndpoint"]);

                    k.TopicEndpoint<OnLanguageUserAttachedMessage>("users.language-attached", "object-service", e =>
                    {
                        e.ConfigureConsumer<OnLanguageUserAttachedConsumer>(context);
                        e.CreateIfMissing();
                    });
                });

                x.AddConsumer<OnLanguageUserAttachedConsumer>();
            });
        });

    }
}