using MediatR;
using Application.Classrooms.Dtos;

namespace Application.Classrooms;

public record ListClassroomsQuery(Guid TenantId) : IRequest<ListClassroomsResponse>;
