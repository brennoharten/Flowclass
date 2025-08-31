namespace Application.Attendance.Dtos;

public record AttendanceDto(Guid Id, Guid ClassroomId, DateTime Start, DateTime End);
public record ListAttendancesRequest(DateTime FromUtc, DateTime ToUtc);
public record ListAttendancesResponse(IReadOnlyList<AttendanceDto> Attendances);

