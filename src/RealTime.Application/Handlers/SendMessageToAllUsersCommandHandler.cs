using RealTime.Application.Contracts.Commands;
using RealTime.Application.Contracts.Exchanges.Response;
using RealTime.Application.Interfaces.Hubs;
using RealTime.Application.Interfaces.Wrappers;

namespace RealTime.Application.Handlers;

public class SendMessageToAllUsersCommandHandler : IRequestHandlerWrapper<SendMessageToAllUsersCommand, SendMessageToUserResponse>
{
  private readonly IChatHub _chatHub;

  public SendMessageToAllUsersCommandHandler(IChatHub chatHub)
  {
    _chatHub = chatHub;
  }

  public async Task<SendMessageToUserResponse> Handle(SendMessageToAllUsersCommand request, CancellationToken cancellationToken)
  {
    await _chatHub.SendMessageToAllUsers(request.Message, cancellationToken);
    return new SendMessageToUserResponse(true, "Message sent");
  }
}