using System.Globalization;
using MassTransit;
using Microservices.Core.Messages.Languages;
using Microservices.Core.Messages.Users;
using Microservices.Users.Core.Services;

namespace Microservices.Users.Application.Consumers;

public class OnLanguageCreatedConsumer (IUserService userService, ITopicProducer<OnLanguageUserAttachedMessage> producer) : IConsumer<OnLanguageCreatedMessage>
{
    public async Task Consume(ConsumeContext<OnLanguageCreatedMessage> context)
    {
        var request = context.Message;
        
        var registered = await userService.RegisterLanguageAsync(request.UserId, request.LanguageId);
        
        var message = new OnLanguageUserAttachedMessage()
        {
            LanguageId = request.LanguageId,
            Status = registered ? DateTime.Now.ToString(CultureInfo.InvariantCulture) : "error"
        };

        await producer.Produce(message);
    }
}