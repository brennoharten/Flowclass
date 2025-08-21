using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> b)
    {
        b.ToTable("payments");
        b.HasKey(x => x.Id);
        b.HasIndex(x => x.ExternalReference).IsUnique();

        b.Property(x => x.Amount).HasColumnType("decimal(10,2)").IsRequired();
        b.Property(x => x.Currency).HasMaxLength(10).IsRequired();
        b.Property(x => x.Status).HasConversion<int>().IsRequired();
        b.Property(x => x.CreatedAt).IsRequired();

        // Relações
        b.HasOne(x => x.User)
         .WithMany(u => u.Payments)
         .HasForeignKey(x => x.UserId)
         .OnDelete(DeleteBehavior.Restrict);
    }
}
