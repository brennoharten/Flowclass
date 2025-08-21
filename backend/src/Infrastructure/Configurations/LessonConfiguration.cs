using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> b)
    {
        b.ToTable("lessons");
        b.HasKey(x => x.Id);

        b.Property(x => x.StartTime).IsRequired();
        b.Property(x => x.EndTime).IsRequired();
        b.Property(x => x.CreatedAt).IsRequired();

        // Relações
        b.HasOne(x => x.Tenant)
         .WithMany()
         .HasForeignKey(x => x.TenantId)
         .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(x => x.Classroom)
         .WithMany(c => c.Lessons)
         .HasForeignKey(x => x.ClassroomId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}