using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ClassroomRepository : IClassroomRepository
{
    private readonly AppDbContext _db;

    public ClassroomRepository(AppDbContext db) => _db = db;

    public async Task<Classroom?> GetAsync(Guid tenantId, Guid classroomId, CancellationToken ct)
    {
        return await _db.Classrooms
            .FirstOrDefaultAsync(c => c.TenantId == tenantId && c.Id == classroomId, ct);
    }

    public async Task<IReadOnlyList<Classroom>> ListAsync(Guid tenantId, CancellationToken ct)
    {
        return await _db.Classrooms
            .Where(c => c.TenantId == tenantId)
            .OrderBy(c => c.Name)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Classroom classroom, CancellationToken ct)
    {
        await _db.Classrooms.AddAsync(classroom, ct);
    }

    public Task SaveChangesAsync(CancellationToken ct)
    {
        return _db.SaveChangesAsync(ct);
    }
}
