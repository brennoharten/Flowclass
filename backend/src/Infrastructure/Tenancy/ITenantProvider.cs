namespace Infrastructure.Tenancy;

public interface ITenantProvider
{
    Guid CurrentTenantId { get; }
    void Set(Guid tenantId);
}