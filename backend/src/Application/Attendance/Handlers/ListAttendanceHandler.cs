using MediatR;
using Application.Attendance.Queries;
using Application.Attendance.Dtos;
using Application.Common.Interfaces.Repositories;

namespace Application.Attendance;

public class ListAttendanceHandler : IRequestHandler<ListAttendanceQuery, IReadOnlyList<AttendanceDto>>
{
    private readonly IAttendanceRepository _attendances;

    public ListAttendanceHandler(IAttendanceRepository attendances) => _attendances = attendances;

    public async Task<IReadOnlyList<AttendanceDto>> Handle(ListAttendanceQuery query, CancellationToken ct)
    {
        var list = await _attendances.GetByLessonAsync(query.TenantId, query.LessonId, ct);

        return list.Select(a =>
            new AttendanceDto(a.Id, a.LessonId, a.StudentId, a.Student.Name, a.CreatedAt)
        ).ToList();
    }
}
