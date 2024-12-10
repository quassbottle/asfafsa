using Microservices.Languages.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Languages.Persistence;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<LanguageEntity> Languages { get; set; }
}