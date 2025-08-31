using MediatR;
using Application.Attendance.Dtos;

namespace Application.Attendance.Queries;

public record ListAttendanceQuery(Guid TenantId, ListAttendancesRequest Request) : IRequest<ListAttendancesResponse>;