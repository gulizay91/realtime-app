using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace RealTime.Infrastructure.Clients;

public class BaseHttpClient(HttpClient httpClient, ILogger<BaseHttpClient> logger) : IBaseHttpClient
{
  public async Task<T?> GetAsync<T>(HttpRequestMessage requestMessage)
  {
    try
    {
      var response = await httpClient.SendAsync(requestMessage);
      response.EnsureSuccessStatusCode();

      await using var contentStream = await response.Content.ReadAsStreamAsync();
      return await JsonSerializer.DeserializeAsync<T>(contentStream);
    }
    catch (HttpRequestException ex)
    {
      logger.LogError(ex, $"Request {requestMessage.Method} to {requestMessage.RequestUri} failed");
      throw;
    }
  }
  
  public async Task<T?> PostAsync<T, TR>(HttpRequestMessage requestMessage, TR content)
  {
    try
    {
      requestMessage.Content = JsonContent.Create(content);

      var response = await httpClient.SendAsync(requestMessage);
      response.EnsureSuccessStatusCode();

      await using var contentStream = await response.Content.ReadAsStreamAsync();
      return await JsonSerializer.DeserializeAsync<T>(contentStream);
    }
    catch (HttpRequestException ex)
    {
      logger.LogError(ex, $"Request {requestMessage.Method} to {requestMessage.RequestUri} failed");
      throw;
    }
  }
  
}