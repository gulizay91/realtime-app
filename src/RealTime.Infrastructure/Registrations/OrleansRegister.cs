using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Configuration;

namespace RealTime.Infrastructure.Registrations;

public static class OrleansRegister
{
  public static void RegisterOrleansHost(this IServiceCollection serviceCollection)
  {
    serviceCollection.AddOrleans(siloBuilder =>
    {
      siloBuilder.Configure<ClusterOptions>(options =>
      {
        options.ClusterId = "realtime-silo-cluster";
        options.ServiceId = "realtime-silo-host";
      });
      siloBuilder.AddMemoryGrainStorageAsDefault();
    });
  }
}