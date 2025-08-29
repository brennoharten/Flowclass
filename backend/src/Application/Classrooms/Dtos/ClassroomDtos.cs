namespace Application.Classrooms.Dtos;

public record CreateClassroomRequest(string Name, Guid TeacherId);
public record ClassroomDto(Guid Id, string Name, Guid TeacherId);
public record ListClassroomsResponse(IReadOnlyList<ClassroomDto> Classrooms);
