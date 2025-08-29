using Domain.Entities;

namespace Application.Common.Interfaces;

public interface ILessonRepository
{
    Task<bool> HasOverlapAsync(Guid tenantId, Guid classroomId, DateTime start, DateTime end, CancellationToken ct);
    Task AddAsync(Lesson lesson, CancellationToken ct);
    Task<IReadOnlyList<Lesson>> ListByRangeAsync(Guid tenantId, DateTime from, DateTime to, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}

