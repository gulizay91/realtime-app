using RealTime.Application.Contracts.Exchanges.Response;
using RealTime.Application.Interfaces.Wrappers;

namespace RealTime.Application.Contracts.Commands;

public record SendMessageToAllUsersCommand : IRequestWrapper<SendMessageToUserResponse>
{
  public string Message { get; init; }
}