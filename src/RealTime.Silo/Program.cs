using System.Net;
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
#if DEBUG
  siloBuilder.UseLocalhostClustering(
    siloPort: 11111,
    gatewayPort: 30000);
#else
  siloBuilder.ConfigureEndpoints(
      siloPort: 11111,
      gatewayPort: 30000,
      listenOnAnyHostAddress: true)
    .UseDevelopmentClustering(options =>
    {
      var ip = GetDockerIPAddress();
      options.PrimarySiloEndpoint = new IPEndPoint(ip, 11111);
    });
#endif
  
  siloBuilder
    .UseDashboard(options => { options.Port = 8080; })
    .ConfigureLogging(logging => logging.AddConsole());
});

await builder.RunConsoleAsync();

static IPAddress GetDockerIPAddress()
{
  return Dns.GetHostAddresses(Dns.GetHostName())
    .First(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
}