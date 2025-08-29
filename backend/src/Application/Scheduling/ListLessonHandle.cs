using MediatR;
using Application.Scheduling.Dtos;
using Application.Common.Interfaces;

namespace Application.Scheduling;

public class ListLessonsHandler : IRequestHandler<ListLessonsQuery, ListLessonsResponse>
{
    private readonly ILessonRepository _lessons;
    public ListLessonsHandler(ILessonRepository lessons) => _lessons = lessons;

    public async Task<ListLessonsResponse> Handle(ListLessonsQuery q, CancellationToken ct)
    {
        var items = await _lessons.ListByRangeAsync(q.TenantId, q.Request.FromUtc, q.Request.ToUtc, ct);
        var dto = items.Select(x => new LessonDto(x.Id, x.ClassroomId, x.StartTime, x.EndTime)).ToList();
        return new ListLessonsResponse(dto);
    }
}

