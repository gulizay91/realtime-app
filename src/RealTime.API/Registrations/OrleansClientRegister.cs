namespace RealTime.API.Registrations;

public static class OrleansClientRegister
{
  public static void RegisterOrleansClient(this IServiceCollection serviceCollection)
  {
    serviceCollection.AddOrleansClient(client =>
    {
      client.UseLocalhostClustering(gatewayPort: 30000, serviceId: "realtime-silo", clusterId: "realtime-silo-cluster");
    });
  }
}