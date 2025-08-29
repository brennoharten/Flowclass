using MediatR;
using Application.Scheduling.Dtos;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Scheduling;

public class ScheduleLessonHandler : IRequestHandler<ScheduleLessonCommand, LessonDto>
{
    private readonly ILessonRepository _lessons;
    private readonly IClassroomRepository _classrooms;

    public ScheduleLessonHandler(ILessonRepository lessons, IClassroomRepository classrooms)
        => (_lessons, _classrooms) = (lessons, classrooms);

    public async Task<LessonDto> Handle(ScheduleLessonCommand cmd, CancellationToken ct)
    {
        var r = cmd.Request;
        if (r.EndTimeUtc <= r.StartTimeUtc) throw new InvalidOperationException("Horário inválido.");

        var classroom = await _classrooms.GetAsync(cmd.TenantId, r.ClassroomId, ct)
                        ?? throw new InvalidOperationException("Turma não encontrada.");

        var overlap = await _lessons.HasOverlapAsync(cmd.TenantId, r.ClassroomId, r.StartTimeUtc, r.EndTimeUtc, ct);
        if (overlap) throw new InvalidOperationException("Conflito de horário na turma.");

        var lesson = new Lesson(cmd.TenantId, r.ClassroomId, r.StartTimeUtc, r.EndTimeUtc);
        await _lessons.AddAsync(lesson, ct);
        await _lessons.SaveChangesAsync(ct);

        return new LessonDto(lesson.Id, lesson.ClassroomId, lesson.StartTime, lesson.EndTime);
    }
}

