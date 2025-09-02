namespace Application.Attendance.Dtos;

public record AttendanceDto(Guid Id, Guid LessonId, Guid StudentId, string StudentName, DateTime MarkedAt);
public record ListAttendancesRequest(DateTime FromUtc, DateTime ToUtc);
public record ListAttendancesResponse(IReadOnlyList<AttendanceDto> Attendances);

