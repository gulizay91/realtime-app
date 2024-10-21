using RealTime.Application.Contracts.Commands;
using RealTime.Application.Contracts.Exchanges.Response;
using RealTime.Application.Interfaces.Hubs;
using RealTime.Application.Interfaces.Wrappers;

namespace RealTime.Application.Handlers;

public class SendMessageToUserCommandHandler : IRequestHandlerWrapper<SendMessageToUserCommand, SendMessageToUserResponse>
{
  private readonly IChatHub _chatHub;

  public SendMessageToUserCommandHandler(IChatHub chatHub)
  {
    _chatHub = chatHub;
  }

  public async Task<SendMessageToUserResponse> Handle(SendMessageToUserCommand request, CancellationToken cancellationToken)
  {
    await _chatHub.SendMessageToUser(request.UserId, request.Message, cancellationToken);
    return new SendMessageToUserResponse(true, "Message sent");
  }
}