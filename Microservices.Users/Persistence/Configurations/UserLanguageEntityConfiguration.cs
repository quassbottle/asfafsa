using Microservices.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservices.Users.Persistence.Configurations;

public class UserLanguageEntityConfiguration : IEntityTypeConfiguration<UserLanguageEntity>
{
    public void Configure(EntityTypeBuilder<UserLanguageEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder
            .HasOne(e => e.User)
            .WithMany(e => e.Languages)
            .HasForeignKey(e => e.UserId);
    }
}