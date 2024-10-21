using RealTime.Domain.Interfaces;

namespace RealTime.Infrastructure.Grains;

public class UserGrain : Grain, IUserGrain
{
  private string _username;
  private string? _connectionId;
  
  public Task SetUsername(string username)
  {
    _username = username;
    return Task.CompletedTask;
  }

  public Task<string> GetUsername()
  {
    return Task.FromResult(_username);
  }
  
  public Task SetConnectionId(string? connectionId)
  {
    _connectionId = connectionId;
    return Task.CompletedTask;
  }

  public Task<string?> GetConnectionId()
  {
    return Task.FromResult(_connectionId);
  }
}