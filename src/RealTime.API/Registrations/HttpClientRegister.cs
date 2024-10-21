using RealTime.Infrastructure.Handlers;

namespace RealTime.API.Registrations;

public static class HttpClientRegister
{
  public static void RegisterHttpClients(this IServiceCollection serviceCollection, IConfiguration configuration)
  {
    serviceCollection.AddTransient<RequestResponseLoggingHandler>();
  }

}