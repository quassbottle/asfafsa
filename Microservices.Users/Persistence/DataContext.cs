using Microservices.Users.Core.Entities;
using Microservices.Users.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Users.Persistence;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserLanguageEntityConfiguration());
    }
    
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserLanguageEntity> UserLanguages { get; set; }
}