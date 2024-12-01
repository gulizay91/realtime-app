using System.Net;

namespace RealTime.API.Registrations;

public static class OrleansClientRegister
{
  public static void RegisterOrleansClient(this IServiceCollection serviceCollection)
  {
#if DEBUG
    serviceCollection.AddOrleansClient(client =>
    {
      client.UseLocalhostClustering(gatewayPort: 30000, serviceId: "realtime-silo-host", clusterId: "realtime-silo-cluster");
    });
#else
    var gatewayAddress = Environment.GetEnvironmentVariable("ORLEANS_SILO_ADDRESS") ?? "127.0.0.1";

    serviceCollection.AddOrleansClient(client =>
    {
      client.Configure<Orleans.Configuration.ClusterOptions>(options =>
      {
        options.ClusterId = "realtime-silo-cluster";
        options.ServiceId = "realtime-silo-host";
      });
      client.UseStaticClustering(options =>
      {
        options.Gateways.Add(new Uri($"gwy.tcp://{gatewayAddress}:30000"));
      });
    });
#endif
  }
}