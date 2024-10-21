using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RealTime.Application.Contracts.Exchanges.Request;
using RealTime.Application.Contracts.Exchanges.Response;
using RealTime.Application.Interfaces.SmsServices.SmsProviders;
using RealTime.Infrastructure.Configurations;
using Twilio.Rest.Api.V2010.Account;

namespace RealTime.Infrastructure.SmsServices.SmsProviders.Twilio;

public class TwilioSmsProvider : ITwilioSmsProvider
{
  private readonly ILogger<TwilioSmsProvider> _logger;
  private readonly TwilioSettings _twilioSettings;
  
  public TwilioSmsProvider(ILogger<TwilioSmsProvider> logger, IOptions<TwilioSettings> twilioSettings)
  {
    _logger = logger;
    _twilioSettings = twilioSettings.Value;
  }
  
  public bool IsAvailable(string phoneNumberCountryCode) => _twilioSettings.SmsProviderSettings.Enable && _twilioSettings.SmsProviderSettings.ActiveCountryCodes.Contains(phoneNumberCountryCode);
  
  public async Task<SendSmsResponse> SendSmsAsync(SendSmsRequest sendSmsRequest)
  {
    try
    {
      var messageResource = await MessageResource.CreateAsync(
        to: new global::Twilio.Types.PhoneNumber(sendSmsRequest.Receiver.PhoneNumber),
        from: new global::Twilio.Types.PhoneNumber(_twilioSettings.PhoneNumber),
        body: sendSmsRequest.TextMessage
      );

      var message = $"SMS sent: {messageResource.Sid}";
      _logger.LogInformation(message);
      return new SendSmsResponse(true, message);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error sending SMS for receiver {0}", sendSmsRequest.Receiver.PhoneNumber);
      return new SendSmsResponse(false, ex.Message);
    }
  }
}