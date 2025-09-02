using MediatR;
using Application.Attendance.Dtos;

namespace Application.Attendance.Commands;

public record MarkAttendanceCommand(Guid TenantId, Guid LessonId, string StudentEmail) 
    : IRequest<AttendanceDto>;
