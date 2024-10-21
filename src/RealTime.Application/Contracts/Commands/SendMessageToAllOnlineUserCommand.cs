using RealTime.Application.Contracts.Exchanges.Response;
using RealTime.Application.Interfaces.Wrappers;

namespace RealTime.Application.Contracts.Commands;

public record SendMessageToAllOnlineUserCommand : IRequestWrapper<SendMessageToUserResponse>
{
  public string Message { get; init; }
}