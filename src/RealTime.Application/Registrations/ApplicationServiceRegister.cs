using Microsoft.Extensions.DependencyInjection;
using RealTime.Application.Services.User;

namespace RealTime.Application.Registrations;

public static class ApplicationServiceRegister
{
  public static void RegisterApplicationServices(this IServiceCollection serviceCollection)
  {
    serviceCollection.AddScoped<IUserService, UserService>();
  }
}