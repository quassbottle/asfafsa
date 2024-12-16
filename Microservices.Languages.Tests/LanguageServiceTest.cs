using MassTransit;
using Microservices.Languages.Core.DTO;
using Microservices.Languages.Core.Services;

namespace Microservices.Languages.Tests;

public class LanguageServiceTest(LanguageServiceServiceProviderFixture languageServiceServiceProviderFixture) : IClassFixture<LanguageServiceServiceProviderFixture>
{
    [Fact]
    public async Task GenerateLanguages_ExpectKafkaToWork()
    {
        var busControl = languageServiceServiceProviderFixture.GetService<IBusControl>();
        await busControl.StartAsync();

        var ids = new List<int>();
        
        var _languageService = languageServiceServiceProviderFixture.GetService<ILanguageService>();
        
        var iterations = 10_000;

        for (var i = 0; i < iterations; i++)
        {
            var result = await _languageService.CreateAsync(new LanguageCreateDTO
            {
                LengthOfCourse = 0, Name = Guid.NewGuid().ToString(), UserId = 1
            });
            
            ids.Add(result.Id);
        }

        var validCount = 0;
        foreach (var id in ids)
        {
            var candidate = await _languageService.GetByIdAsync(id);
            if (candidate.Status is not null && candidate.Status != "error")
            {
                validCount++;
            }
        }
        
        Assert.Equal(validCount, iterations);
    }
}