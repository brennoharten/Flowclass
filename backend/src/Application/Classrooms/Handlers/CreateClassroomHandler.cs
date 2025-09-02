using MediatR;
using Application.Classrooms.Dtos;
using Application.Common.Interfaces;
using Domain.Entities;
using Application.Common.Interfaces.Repositories;

namespace Application.Classrooms;

public class CreateClassroomHandler : IRequestHandler<CreateClassroomCommand, ClassroomDto>
{
    private readonly IClassroomRepository _repo;

    public CreateClassroomHandler(IClassroomRepository repo) => _repo = repo;

    public async Task<ClassroomDto> Handle(CreateClassroomCommand cmd, CancellationToken ct)
    {
        var r = cmd.Request;
        var classroom = new Classroom(cmd.TenantId, r.Name, r.TeacherId);

        await _repo.AddAsync(classroom, ct);
        await _repo.SaveChangesAsync(ct);

        return new ClassroomDto(classroom.Id, classroom.Name, classroom.TeacherId);
    }
}
