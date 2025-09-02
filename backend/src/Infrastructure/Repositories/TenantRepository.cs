using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TenantRepository : ITenantRepository
{
    private readonly AppDbContext _db;

    public TenantRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Tenant tenant, CancellationToken ct)
    {
        _db.Tenants.Add(tenant);
        await _db.SaveChangesAsync(ct);
    }

    public Task<Tenant?> GetByIdAsync(Guid id, CancellationToken ct)
        => _db.Tenants.FirstOrDefaultAsync(t => t.Id == id, ct);

    public Task<Tenant?> GetByNameAsync(string name, CancellationToken ct)
        => _db.Tenants.FirstOrDefaultAsync(t => t.Name == name, ct);
}
