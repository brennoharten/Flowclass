using MediatR;
using Application.Classrooms.Dtos;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;

namespace Application.Classrooms;

public class ListClassroomsHandler 
    : IRequestHandler<ListClassroomsQuery, ListClassroomsResponse>
{
    private readonly IClassroomRepository _repo;

    public ListClassroomsHandler(IClassroomRepository repo) => _repo = repo;

    public async Task<ListClassroomsResponse> Handle(ListClassroomsQuery q, CancellationToken ct)
    {
        var items = await _repo.ListAsync(q.TenantId, ct);
        var dto = items.Select(x => new ClassroomDto(x.Id, x.Name, x.TeacherId)).ToList();
        return new ListClassroomsResponse(dto);
    }
}

