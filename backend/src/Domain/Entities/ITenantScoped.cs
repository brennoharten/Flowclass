
namespace Domain.Entities;

public interface ITenantScoped
{
    Guid TenantId { get; }
}
