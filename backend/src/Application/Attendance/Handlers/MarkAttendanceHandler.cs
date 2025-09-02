using MediatR;
using Application.Attendance.Commands;
using Application.Attendance.Dtos;
using Application.Common.Interfaces.Repositories;
using AttendanceEntity = Domain.Entities.Attendance;
using Domain.Entities;
using Application.Common.Interfaces;

namespace Application.Attendance;

public class MarkAttendanceHandler : IRequestHandler<MarkAttendanceCommand, AttendanceDto>
{
    private readonly IAttendanceRepository _attendances;
    private readonly IUserRepository _users;

    public MarkAttendanceHandler(IAttendanceRepository attendances, IUserRepository users)
        => (_attendances, _users) = (attendances, users);

    public async Task<AttendanceDto> Handle(MarkAttendanceCommand cmd, CancellationToken ct)
    {
        var student = await _users.GetByEmailAsync(cmd.TenantId, cmd.StudentEmail, ct)
            ?? throw new Exception("Student not found");

        var attendance = new AttendanceEntity(cmd.TenantId, cmd.LessonId, student.Id, AttendanceStatus.Present);

        await _attendances.AddAsync(attendance, ct);

        return new AttendanceDto(attendance.Id, attendance.LessonId, student.Id, student.Name, attendance.CreatedAt);
    }
}
