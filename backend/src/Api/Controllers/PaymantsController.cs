/* using Application.Payments;
using Application.Payments.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("payments")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;
    public PaymentsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("checkout")]
    [Authorize(Roles = "Professor")]
    public async Task<IActionResult> Checkout([FromBody] CheckoutRequest req, CancellationToken ct)
    {
        var res = await _mediator.Send(new CheckoutCommand(req), ct);
        return Ok(res);
    }

    [HttpPost("webhook")]
    [AllowAnonymous]
    public async Task<IActionResult> Webhook([FromBody] PaymentWebhookRequest req, CancellationToken ct)
    {
        await _mediator.Send(new ProcessPaymentWebhookCommand(req), ct);
        return Ok();
    }
}
 */