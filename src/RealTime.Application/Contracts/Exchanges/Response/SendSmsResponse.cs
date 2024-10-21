namespace RealTime.Application.Contracts.Exchanges.Response;

public record SendSmsResponse(bool IsSucceed, string Message) : BaseResponse(IsSucceed, Message);