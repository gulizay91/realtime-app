using RealTime.Application.Contracts.Exchanges.Response;
using RealTime.Application.Interfaces.Wrappers;

namespace RealTime.Application.Contracts.Commands;

public record SendSmsCommand : IRequestWrapper<SendSmsResponse>
{
  public required string PhoneNumber { get; init; }
  public required string Message { get; init; }
}