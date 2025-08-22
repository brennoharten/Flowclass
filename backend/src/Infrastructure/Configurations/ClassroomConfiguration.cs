using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ClassroomConfiguration : IEntityTypeConfiguration<Classroom>
{
    public void Configure(EntityTypeBuilder<Classroom> b)
    {
        b.ToTable("classrooms");
        b.HasKey(x => x.Id);

        b.Property(x => x.Name).HasMaxLength(200).IsRequired();
        b.Property(x => x.CreatedAt).IsRequired();
        b.Property(x => x.TenantId).IsRequired();

        b.HasOne(x => x.Teacher)
         .WithMany()
         .HasForeignKey(x => x.TeacherId)
         .OnDelete(DeleteBehavior.Restrict);
    }
}