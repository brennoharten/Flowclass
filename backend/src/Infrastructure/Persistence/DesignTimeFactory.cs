using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Infrastructure.Tenancy;

namespace Infrastructure.Persistence;

public class DesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Lê de env var ou fallback
        var conn = Environment.GetEnvironmentVariable("CONNSTR")
                   ?? "Host=localhost;Port=5432;Database=saas_dev;Username=dev;Password=devpass";

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(conn)
            .UseSnakeCaseNamingConvention()
            .Options;

        // Tenant 00000000-0000-0000-0000-000000000001 apenas para migrations
        var tenantProvider = new StaticTenantProvider(Guid.Parse("00000000-0000-0000-0000-000000000001"));

        return new AppDbContext(options, tenantProvider);
    }

    private class StaticTenantProvider : ITenantProvider
    {
        public StaticTenantProvider(Guid id) => CurrentTenantId = id;
        public Guid CurrentTenantId { get; }
        public void Set(Guid tenantId) { /* ignorado no design-time */ }
    }
}

