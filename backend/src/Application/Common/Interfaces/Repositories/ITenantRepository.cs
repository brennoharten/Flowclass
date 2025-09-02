using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface ITenantRepository
{
    Task AddAsync(Tenant tenant, CancellationToken ct);
    Task<Tenant?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<Tenant?> GetByNameAsync(string name, CancellationToken ct);
}
