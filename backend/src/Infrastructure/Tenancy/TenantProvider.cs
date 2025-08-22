namespace Infrastructure.Tenancy;

public class TenantProvider : ITenantProvider
{
    private static readonly AsyncLocal<Guid> _current = new();
    public Guid CurrentTenantId => _current.Value;
    public void Set(Guid tenantId) => _current.Value = tenantId;
}