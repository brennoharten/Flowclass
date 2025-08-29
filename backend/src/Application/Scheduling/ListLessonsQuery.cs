using MediatR;
using Application.Scheduling.Dtos;

namespace Application.Scheduling;
public record ListLessonsQuery(Guid TenantId, ListLessonsRequest Request) : IRequest<ListLessonsResponse>;

