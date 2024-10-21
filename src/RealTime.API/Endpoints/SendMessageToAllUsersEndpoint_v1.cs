using FastEndpoints;
using MediatR;
using RealTime.Application.Contracts.Commands;

namespace RealTime.API.Endpoints;

public class SendMessageToAllUsersEndpoint_v1: Endpoint<SendMessageToAllUsersCommand>
{
  private readonly IMediator _mediator;

  public SendMessageToAllUsersEndpoint_v1(IMediator mediator)
  {
    _mediator = mediator;
  }

  public override void Configure()
  {
    Post("/chathub/send-all-users");
    Version(1);
    AllowAnonymous();
  }

  public override async Task HandleAsync(SendMessageToAllUsersCommand req, CancellationToken ct)
  {
    var response = await _mediator.Send(req, ct);
    await SendOkAsync(response, ct);
  }
}