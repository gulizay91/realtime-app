namespace RealTime.Application.Contracts.Exchanges.Response;

public abstract record BaseResponse(bool IsSucceed, string Message)
{
  public bool IsSucceed { get; init; } = IsSucceed;
  public string Message { get; set; } = Message;
}