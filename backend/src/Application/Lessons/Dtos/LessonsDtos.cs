namespace Application.Lessons.Dtos;

public record CreateLessonRequest(Guid ClassroomId, DateTime StartTimeUtc, DateTime EndTimeUtc);
public record LessonDto(Guid Id, Guid ClassroomId, DateTime Start, DateTime End);
public record ListLessonsRequest(Guid ClassroomId, DateTime FromUtc, DateTime ToUtc);
public record ListLessonsResponse(IReadOnlyList<LessonDto> Lessons);

