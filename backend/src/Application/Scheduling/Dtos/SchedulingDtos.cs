namespace Application.Scheduling.Dtos;

public record ScheduleLessonRequest(Guid ClassroomId, DateTime StartTimeUtc, DateTime EndTimeUtc);
public record LessonDto(Guid Id, Guid ClassroomId, DateTime Start, DateTime End);
public record ListLessonsRequest(DateTime FromUtc, DateTime ToUtc);
public record ListLessonsResponse(IReadOnlyList<LessonDto> Lessons);

