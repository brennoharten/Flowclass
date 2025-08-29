using MediatR;
using Application.Classrooms.Dtos;

namespace Application.Classrooms;

public record CreateClassroomCommand(Guid TenantId, CreateClassroomRequest Request) : IRequest<ClassroomDto>;
