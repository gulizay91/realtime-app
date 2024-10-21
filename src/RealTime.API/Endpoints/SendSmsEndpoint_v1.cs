using FastEndpoints;
using MediatR;
using RealTime.Application.Contracts.Commands;
using RealTime.Application.Contracts.Exchanges.Response;

namespace RealTime.API.Endpoints;

//[HttpPost("/send-sms")]
public class SendSmsEndpoint_v1 : Endpoint<SendSmsCommand, SendSmsResponse>
{
  private readonly IMediator _mediator;

  public SendSmsEndpoint_v1(IMediator mediator)
  {
    _mediator = mediator;
  }

  public override void Configure()
  {
    Post("/send-sms");
    Version(1);
    AllowAnonymous();
  }

  public override async Task HandleAsync(SendSmsCommand req, CancellationToken ct)
  {
    var response = await _mediator.Send(req, ct);
    await SendAsync(response, cancellation: ct);
  }
}