using Application.Scheduling;
using Application.Scheduling.Dtos;
using Infrastructure.Tenancy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("lessons")]
[Authorize]
public class LessonsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITenantProvider _tenant;

    public LessonsController(IMediator mediator, ITenantProvider tenant)
        => (_mediator, _tenant) = (mediator, tenant);

    [HttpPost]
    public async Task<IActionResult> Schedule([FromBody] ScheduleLessonRequest req, CancellationToken ct)
    {
        var res = await _mediator.Send(new ScheduleLessonCommand(_tenant.CurrentTenantId, req), ct);
        return Ok(res);
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] DateTime fromUtc, [FromQuery] DateTime toUtc, CancellationToken ct)
    {
        var res = await _mediator.Send(new ListLessonsQuery(_tenant.CurrentTenantId, new ListLessonsRequest(fromUtc, toUtc)), ct);
        return Ok(res);
    }
}

