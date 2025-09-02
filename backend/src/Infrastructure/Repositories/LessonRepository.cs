using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly AppDbContext _db;
    public LessonRepository(AppDbContext db) => _db = db;

    public Task<bool> HasOverlapAsync(Guid tenantId, Guid classroomId, DateTime start, DateTime end, CancellationToken ct)
        => _db.Lessons.AnyAsync(l =>
               l.TenantId == tenantId && l.ClassroomId == classroomId &&
               ((start < l.EndTime) && (end > l.StartTime)), ct);

    public async Task AddAsync(Lesson lesson, CancellationToken ct)
        => await _db.Lessons.AddAsync(lesson, ct);

    public async Task<IReadOnlyList<Lesson>> ListByRangeAsync(Guid tenantId, DateTime from, DateTime to, CancellationToken ct)
        => await _db.Lessons
            .Where(l => l.TenantId == tenantId && l.StartTime >= from && l.StartTime <= to)
            .OrderBy(l => l.StartTime)
            .ToListAsync(ct);

    public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}

