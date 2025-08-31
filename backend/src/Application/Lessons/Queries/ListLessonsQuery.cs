using MediatR;
using Application.Lessons.Dtos;

namespace Application.Lessons.Queries;
public record ListLessonsQuery(Guid TenantId, ListLessonsRequest Request) : IRequest<ListLessonsResponse>;

