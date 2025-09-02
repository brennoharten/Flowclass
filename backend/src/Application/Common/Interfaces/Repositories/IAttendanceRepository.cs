using AttendanceEntity = Domain.Entities.Attendance;

namespace Application.Common.Interfaces.Repositories;

public interface IAttendanceRepository
{
    Task AddAsync(AttendanceEntity attendance, CancellationToken ct);
    Task<List<AttendanceEntity>> GetByLessonAsync(Guid tenantId, Guid lessonId, CancellationToken ct);
}
