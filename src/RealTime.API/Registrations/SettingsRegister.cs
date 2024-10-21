using RealTime.Infrastructure.Configurations;

namespace RealTime.API.Registrations;

public static class SettingsRegister
{
  public static void RegisterSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
  {
    serviceCollection.Configure<TwilioSettings>(
      configuration.GetSection("Services:TwilioService"));
    
    // Validation for settings
    serviceCollection.AddOptions<TwilioSettings>()
      .Bind(configuration.GetSection("Services:TwilioService"))
      .ValidateDataAnnotations()
      .ValidateOnStart();
  }
}