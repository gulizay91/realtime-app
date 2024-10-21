namespace RealTime.Application.Contracts.Exchanges.Response;

public record SendMessageToUserResponse(bool IsSucceed, string Message) : BaseResponse(IsSucceed, Message);