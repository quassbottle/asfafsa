using Microservices.Languages.Application.Services;
using Microservices.Languages.Core.Services;
using Microservices.Languages.Persistence;
using Microservices.Languages.Supporting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Languages.Tests;

public class LanguageServiceServiceProviderFixture : IDisposable
{
    private readonly IServiceScope _scope;

    private IConfiguration _configuration;
    private IConfiguration Configuration
    {
        get
        {
            if (_configuration is not null) return _configuration;
            
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();

            return _configuration;
        }
    }

    public LanguageServiceServiceProviderFixture()
    {
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddDbContext<DataContext>(opt =>
            opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        serviceCollection.AddTransient<ILanguageService, LanguageService>();
        
        serviceCollection.AddKafka(Configuration);
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _scope = serviceProvider.CreateScope();
    }
    
    public void Dispose()
    {
        _scope.Dispose();
    }
    
    public T GetService<T>()
    {
        return _scope.ServiceProvider.GetRequiredService<T>();
    }
}