using Application.Attendance;
using Application.Attendance.Dtos;
using Application.Attendance.Queries;
using Application.Attendance.Commands;
using Infrastructure.Tenancy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Common.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("attendance")]
[Authorize]
public class AttendanceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITenantProvider _tenant;
    private readonly IUserContext _user;

    public AttendanceController(IMediator mediator, ITenantProvider tenant, IUserContext user)
        => (_mediator, _tenant, _user) = (mediator, tenant, user);

    [HttpPost("{lessonId}/mark")]
    public async Task<IActionResult> Mark(Guid lessonId, CancellationToken ct)
    {
        var res = await _mediator.Send(new MarkAttendanceCommand(_user.TenantId, lessonId, _user.Email), ct);
        return Ok(res);
    }


    [HttpGet("{lessonId}")]
    [Authorize(Roles = "Professor")]
    public async Task<IActionResult> List(Guid lessonId, CancellationToken ct)
    {
        var res = await _mediator.Send(new ListAttendanceQuery(_tenant.CurrentTenantId, lessonId), ct);
        return Ok(res);
    }
}
