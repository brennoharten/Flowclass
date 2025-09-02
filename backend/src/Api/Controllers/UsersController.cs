using Application.Common.Interfaces;
using Application.Users;
using Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContext _user;

    public UsersController(IMediator mediator, IUserContext user)
        => (_mediator, _user) = (mediator, user);

    [HttpGet("me")]
    public async Task<IActionResult> Me(CancellationToken ct)
    {
        var tenantId = _user.TenantId;
        var email = _user.Email;

        var res = await _mediator.Send(new GetCurrentUserQuery(tenantId, email), ct);
        return Ok(res);
    }


}
