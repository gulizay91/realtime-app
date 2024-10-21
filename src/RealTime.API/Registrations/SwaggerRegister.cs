using FastEndpoints.Swagger;

namespace RealTime.API.Registrations;

public static class SwaggerRegister
{
  public static void RegisterSwagger(this IServiceCollection serviceCollection)
  {
    var versions = GetApiVersions();
    foreach (var version in versions)
    {
      serviceCollection.SwaggerDocument(o =>
      {
        o.MaxEndpointVersion = version;
        o.DocumentSettings = s =>
        {
          s.DocumentName = $"v{version}";
          s.Title = $"RealTime API v{version}";
          s.Version = $"v{version}";
        };
      });
    }
  }

  public static void UseSwagger(this IApplicationBuilder applicationBuilder)
  {
    applicationBuilder.UseSwaggerGen();
    // Configure Swagger UI to display each version dynamically
    applicationBuilder.UseSwaggerUI(c =>
    {
      var versions = GetApiVersions();
      foreach (var version in versions)
      {
        // Add Swagger endpoints dynamically for each version
        c.SwaggerEndpoint($"/swagger/v{version}/swagger.json", $"RealTime API v{version}");
      }
    });
  }
  
  private static IEnumerable<int> GetApiVersions()
  {

    return [1, 2];
  }
}