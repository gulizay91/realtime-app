using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RealTime.Application.Interfaces.Hubs;
using RealTime.Infrastructure.Hubs;

namespace RealTime.Infrastructure.Registrations;

public static class SignalRHubRegister
{
  public static void RegisterSignalRHub(this IServiceCollection serviceCollection)
  {
    serviceCollection.AddSignalR();
    
    serviceCollection.AddScoped<IChatHub, ChatHub>();
  }
  
  public static void UseSignalRHub(this IApplicationBuilder applicationBuilder)
  {
    applicationBuilder.UseEndpoints(endpoints =>
    {
      endpoints.MapHub<ChatHub>("/chatHub");
    });
  }
}