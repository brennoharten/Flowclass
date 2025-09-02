using MediatR;
using Application.Lessons.Dtos;

namespace Application.Lessons.Commands;

public record CreateLessonCommand(Guid TenantId, CreateLessonRequest Request) : IRequest<LessonDto>;

