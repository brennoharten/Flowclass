using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> b)
    {
        b.ToTable("refresh_tokens");
        b.HasKey(x => x.Id);
        b.HasIndex(x => x.Token).IsUnique();

        b.Property(x => x.Token).HasMaxLength(500).IsRequired();
        b.Property(x => x.ExpiresAt).IsRequired();
        b.Property(x => x.CreatedAt).IsRequired();
        b.Property(x => x.RevokedAt).IsRequired(false);

        b.HasOne(x => x.User)
         .WithMany(u => u.RefreshTokens)
         .HasForeignKey(x => x.UserId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}