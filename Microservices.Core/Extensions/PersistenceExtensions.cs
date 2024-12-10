using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microservices.Core.Extensions;

public static class PersistenceExtensions
{
    public static void MigrateDatabase<TDataContext>(this IHost host) where TDataContext: DbContext
    {
        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetService<TDataContext>();
        context?.Database.Migrate();
    }
}