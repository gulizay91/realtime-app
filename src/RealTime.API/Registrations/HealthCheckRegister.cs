using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace RealTime.API.Registrations;

public static class HealthCheckRegister
{
  public static void UseHealthCheckEndpoints(this IApplicationBuilder applicationBuilder)
  {
    //for liveness probe
    applicationBuilder.UseEndpoints(endpoints =>
    {
      endpoints.MapHealthChecks("/health", new HealthCheckOptions
      {
        Predicate = _ => false
      });
    });

    //for readiness probe 
    applicationBuilder.UseEndpoints(endpoints =>
    {
      endpoints.MapHealthChecks("/ready", new HealthCheckOptions
      {
        Predicate = check => check.Tags.Contains("ready")
      });
    });
  }
}