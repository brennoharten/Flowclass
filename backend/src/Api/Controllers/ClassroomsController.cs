using Application.Classrooms;
using Application.Classrooms.Dtos;
using Infrastructure.Tenancy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("classrooms")]
[Authorize]
public class ClassroomsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITenantProvider _tenant;

    public ClassroomsController(IMediator mediator, ITenantProvider tenant)
        => (_mediator, _tenant) = (mediator, tenant);

    [HttpPost]
    [Authorize(Roles = "Professor")]
    public async Task<IActionResult> Create([FromBody] CreateClassroomRequest req, CancellationToken ct)
    {
        var res = await _mediator.Send(new CreateClassroomCommand(_tenant.CurrentTenantId, req), ct);
        return Ok(res);
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken ct)
    {
        var res = await _mediator.Send(new ListClassroomsQuery(_tenant.CurrentTenantId), ct);
        return Ok(res);
    }
}
