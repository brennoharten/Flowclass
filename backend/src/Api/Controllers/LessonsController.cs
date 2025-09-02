using Application.Lessons;
using Application.Lessons.Commands;
using Application.Lessons.Dtos;
using Application.Lessons.Queries;
using Infrastructure.Tenancy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("classrooms/{classroomId}/lessons")]
[Authorize]
public class LessonsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITenantProvider _tenant;

    public LessonsController(IMediator mediator, ITenantProvider tenant)
        => (_mediator, _tenant) = (mediator, tenant);

    [HttpPost]
    [Authorize(Roles = "Professor")]
    public async Task<IActionResult> Create([FromBody] CreateLessonRequest req, CancellationToken ct)
    {
        var res = await _mediator.Send(new CreateLessonCommand(_tenant.CurrentTenantId, req), ct);
        return Ok(res);
    }

    [HttpGet]
    public async Task<IActionResult> List(ListLessonsRequest req, CancellationToken ct)
    {
        var res = await _mediator.Send(new ListLessonsQuery(_tenant.CurrentTenantId, req), ct);
        return Ok(res);
    }
}
