using Microsoft.Extensions.Options;
using RealTime.Infrastructure.Configurations;
using Twilio;

namespace RealTime.API.WorkerServices;

public class ApplicationLifetimeService : IHostedService
{
  private readonly IHostApplicationLifetime _applicationLifetime;
  private readonly ILogger<ApplicationLifetimeService> _logger;
  private readonly IServiceProvider _services;
  private readonly TwilioSettings _twilioSettings;

  public ApplicationLifetimeService(
    IHostApplicationLifetime applicationLifetime,
    ILogger<ApplicationLifetimeService> logger,
    IServiceProvider services,
    IOptions<TwilioSettings> twilioSettings)
  {
    _applicationLifetime = applicationLifetime;
    _logger = logger;
    _services = services;
    _twilioSettings = twilioSettings.Value;
  }
  
  public Task StartAsync(CancellationToken cancellationToken)
  {
    _applicationLifetime.ApplicationStarted.Register(async () =>
    {
      _logger.LogInformation("Application started.");
      TwilioClient.Init(_twilioSettings.AccountSID, _twilioSettings.AuthToken);
    });
    
    // register a callback that sleeps for 30 seconds
    _applicationLifetime.ApplicationStopping.Register(() =>
    {
      _logger.LogInformation("SIGTERM received, waiting for 30 seconds");
      Thread.Sleep(30_000);
      _logger.LogInformation("Termination delay complete, continuing stopping process");
    });
    return Task.CompletedTask;
  }
  
  public Task StopAsync(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }
}