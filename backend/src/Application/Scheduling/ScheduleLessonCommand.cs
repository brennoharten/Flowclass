using MediatR;
using Application.Scheduling.Dtos;

namespace Application.Scheduling;
public record ScheduleLessonCommand(Guid TenantId, ScheduleLessonRequest Request) : IRequest<LessonDto>;

