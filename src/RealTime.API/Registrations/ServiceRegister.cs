using RealTime.API.WorkerServices;
using RealTime.Application.Interfaces.SmsServices;
using RealTime.Application.Interfaces.SmsServices.SmsProviders;
using RealTime.Infrastructure.Clients;
using RealTime.Infrastructure.SmsServices;
using RealTime.Infrastructure.SmsServices.SmsProviders.Twilio;

namespace RealTime.API.Registrations;

public static class ServiceRegister
{
  public static void RegisterServices(this IServiceCollection serviceCollection)
  {
    serviceCollection.AddHttpClient();
    serviceCollection.AddScoped<ISmsService, SmsService>();
    serviceCollection.AddScoped<IBaseHttpClient, BaseHttpClient>();
    serviceCollection.AddScoped<ITwilioSmsProvider, TwilioSmsProvider>();
    
    serviceCollection.AddHostedService<ApplicationLifetimeService>();
  }
}