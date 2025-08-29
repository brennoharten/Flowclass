
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Tenancy;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly ITenantProvider _tenant;

    public AppDbContext(DbContextOptions<AppDbContext> options, ITenantProvider tenant) : base(options)
        => _tenant = tenant;

    public DbSet<User> Users => Set<User>();
    public DbSet<Tenant> Tenants { get; set; } = null!;

    public DbSet<Classroom> Classrooms => Set<Classroom>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // snake_case (bom pra Postgres)
        modelBuilder.UseSerialColumns(); // usa identity/serial conforme provedor
        modelBuilder.HasPostgresExtension("uuid-ossp");

        // Aplicar configurações por assembly (IEntityTypeConfiguration<>)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<RefreshToken>(eb =>
        {
            eb.HasKey(r => r.Id);
            eb.Property(r => r.Token).IsRequired().HasMaxLength(200);
            eb.HasIndex(r => r.Token).IsUnique();
            eb.HasOne(r => r.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        // Filtro global de tenant (para entidades que implementam ITenantScoped)
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITenantScoped).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(AppDbContext)
                    .GetMethod(nameof(SetTenantFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                    !.MakeGenericMethod(entityType.ClrType);

                method.Invoke(null, new object[] { modelBuilder, _tenant });
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    private static void SetTenantFilter<TEntity>(ModelBuilder mb, ITenantProvider tenant)
        where TEntity : class, ITenantScoped
    {
        mb.Entity<TEntity>().HasQueryFilter(e => e.TenantId == tenant.CurrentTenantId);
    }
}
