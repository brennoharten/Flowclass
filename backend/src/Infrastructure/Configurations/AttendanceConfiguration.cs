using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> b)
    {
        b.ToTable("attendances");
        b.HasKey(x => x.Id);

        b.Property(x => x.Status).HasConversion<int>().IsRequired();
        b.Property(x => x.CreatedAt).IsRequired();
        b.Property(x => x.TenantId).IsRequired();

        b.HasOne(x => x.Lesson)
         .WithMany(l => l.Attendances)
         .HasForeignKey(x => x.LessonId)
         .OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Student)
         .WithMany(u => u.Attendances)
         .HasForeignKey(x => x.StudentId)
         .OnDelete(DeleteBehavior.Restrict);
    }
}
