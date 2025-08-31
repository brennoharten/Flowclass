using Application.Attendance.Dtos;
using Application.Attendance.Queries;
using Application.Lessons.Commands;
using Application.Lessons.Dtos;
using Infrastructure.Tenancy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("Attendance")]
[Authorize]
public class AttendanceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITenantProvider _tenant;

    public AttendanceController(IMediator mediator, ITenantProvider tenant)
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
        var res = await _mediator.Send(new ListAttendanceQuery(_tenant.CurrentTenantId, new ListAttendancesRequest(fromUtc, toUtc)), ct);
        return Ok(res);
    }
}

