using RealTime.Application.Contracts.Commands;
using RealTime.Application.Contracts.Exchanges.Request;
using RealTime.Application.Contracts.Exchanges.Response;
using RealTime.Application.Interfaces.SmsServices;
using RealTime.Application.Interfaces.Wrappers;

namespace RealTime.Application.Handlers;

public class SendSmsCommandHandler : IRequestHandlerWrapper<SendSmsCommand, SendSmsResponse>
{
  private readonly ISmsService _smsService;

  public SendSmsCommandHandler(ISmsService smsService)
  {
    _smsService = smsService;
  }

  public async Task<SendSmsResponse> Handle(SendSmsCommand request, CancellationToken cancellationToken)
  {
    var serviceRequest = new SendSmsRequest(new Receiver(request.PhoneNumber), request.Message);
    var response = await _smsService.SendSmsAsync(serviceRequest);

    return response;
  }
}