using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface IClassroomRepository
{
    Task<Classroom?> GetAsync(Guid tenantId, Guid classroomId, CancellationToken ct);
    Task<IReadOnlyList<Classroom>> ListAsync(Guid tenantId, CancellationToken ct);
    Task AddAsync(Classroom classroom, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}
