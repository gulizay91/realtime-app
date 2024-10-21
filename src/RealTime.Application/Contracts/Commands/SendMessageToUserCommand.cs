using RealTime.Application.Contracts.Exchanges.Response;
using RealTime.Application.Interfaces.Wrappers;

namespace RealTime.Application.Contracts.Commands;

public record SendMessageToUserCommand : IRequestWrapper<SendMessageToUserResponse>
{
  public string UserId { get; init; }
  public string Message { get; init; }
}