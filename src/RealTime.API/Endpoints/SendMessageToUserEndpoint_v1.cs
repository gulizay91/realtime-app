using FastEndpoints;
using MediatR;
using RealTime.Application.Contracts.Commands;

namespace RealTime.API.Endpoints;

public class SendMessageToUserEndpoint_v1 : Endpoint<SendMessageToUserCommand>
{
  private readonly IMediator _mediator;

  public SendMessageToUserEndpoint_v1(IMediator mediator)
  {
    _mediator = mediator;
  }

  public override void Configure()
  {
    Post("/chathub/send-user");
    Version(1);
    AllowAnonymous();
  }

  public override async Task HandleAsync(SendMessageToUserCommand req, CancellationToken ct)
  {
    var response = await _mediator.Send(req, ct);
    await SendOkAsync(response, ct);
  }
}