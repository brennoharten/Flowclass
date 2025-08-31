using MediatR;
using Application.Lessons.Dtos;

namespace Application.Lessons.Commands;
public record ScheduleLessonCommand(Guid TenantId, ScheduleLessonRequest Request) : IRequest<LessonDto>;

