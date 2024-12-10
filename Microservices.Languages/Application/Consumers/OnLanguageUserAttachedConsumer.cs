using MassTransit;
using Microservices.Core.Messages.Users;
using Microservices.Languages.Core.Services;

namespace Microservices.Languages.Application.Consumers;

public class OnLanguageUserAttachedConsumer(ILanguageService service) : IConsumer<OnLanguageUserAttachedMessage>
{
    public async Task Consume(ConsumeContext<OnLanguageUserAttachedMessage> context)
    {
        var response = context.Message;

        await service.UpdateStatusAsync(response.LanguageId, response.Status);
    }
}