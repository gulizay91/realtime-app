using RealTime.Application.Contracts.Exchanges.Request;
using RealTime.Application.Contracts.Exchanges.Response;

namespace RealTime.Application.Interfaces.SmsServices;

public interface ISmsService
{
  Task<SendSmsResponse> SendSmsAsync(SendSmsRequest sendSmsRequest);
}