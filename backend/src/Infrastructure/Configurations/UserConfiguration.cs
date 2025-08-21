using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> b)
    {
        b.ToTable("users");
        b.HasKey(x => x.Id);
        b.HasIndex(x => new { x.TenantId, x.Email }).IsUnique();

        b.Property(x => x.Email).HasMaxLength(255).IsRequired();
        b.Property(x => x.PasswordHash).HasMaxLength(255).IsRequired();
        b.Property(x => x.Name).HasMaxLength(200).IsRequired();
        b.Property(x => x.Role).HasConversion<int>().IsRequired(); // salva como int
    }
}

