using Application.Auth;
using Application.Auth.Dtos;
using Infrastructure.Tenancy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITenantProvider _tenant;

    public AuthController(IMediator mediator, ITenantProvider tenant)
        => (_mediator, _tenant) = (mediator, tenant);

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req, CancellationToken ct)
    {
        var res = await _mediator.Send(new RegisterUserCommand(_tenant.CurrentTenantId, req), ct);
        return Ok(res);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req, CancellationToken ct)
    {
        var res = await _mediator.Send(new LoginCommand(_tenant.CurrentTenantId, req), ct);
        return Ok(res);
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest req, CancellationToken ct)
    {
        var res = await _mediator.Send(new RefreshCommand(_tenant.CurrentTenantId, req), ct);
        return Ok(res);
    }
}

