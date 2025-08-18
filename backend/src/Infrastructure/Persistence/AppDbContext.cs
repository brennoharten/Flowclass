
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
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Classroom> Classrooms => Set<Classroom>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<Attendance> Attendances => Set<Attendance>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // snake_case (bom pra Postgres)
        modelBuilder.UseSerialColumns(); // usa identity/serial conforme provedor
        modelBuilder.HasPostgresExtension("uuid-ossp");
        modelBuilder.UseSnakeCaseNamingConvention();

        // Aplicar configurações por assembly (IEntityTypeConfiguration<>)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

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
