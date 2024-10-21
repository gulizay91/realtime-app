using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RealTime.Infrastructure.Registrations;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
  services.RegisterOrleansHost();
});
builder.UseOrleans(siloBuilder =>
{
  siloBuilder.UseLocalhostClustering(
      siloPort: 11111,
      gatewayPort: 30000)
    .UseDashboard(options => { options.Port = 8080; })
    .ConfigureLogging(logging => logging.AddConsole());
});

await builder.RunConsoleAsync();