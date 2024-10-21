using RealTime.Application.Contracts.Exchanges.Request;
using RealTime.Application.Contracts.Exchanges.Response;
using RealTime.Application.Interfaces.SmsServices;
using RealTime.Application.Interfaces.SmsServices.SmsProviders;
using RealTime.Application.Utils;

namespace RealTime.Infrastructure.SmsServices;

public class SmsService : ISmsService
{
  private readonly ITwilioSmsProvider _twilioSmsProvider;
  
  public SmsService(ITwilioSmsProvider twilioSmsProvider)
  {
    _twilioSmsProvider = twilioSmsProvider;
  }

  public async Task<SendSmsResponse> SendSmsAsync(SendSmsRequest sendSmsRequest)
  {
    var countryCode = PhoneNumberUtils.GetCountryCodeFromPhoneNumber(sendSmsRequest.Receiver.PhoneNumber);
    ArgumentException.ThrowIfNullOrWhiteSpace(countryCode, nameof(sendSmsRequest.Receiver.PhoneNumber));

    if(_twilioSmsProvider.IsAvailable(countryCode))
      return await _twilioSmsProvider.SendSmsAsync(sendSmsRequest);

    throw new NotImplementedException($"There is no activated country code for this {nameof(sendSmsRequest.Receiver.PhoneNumber)}!");
  }

}