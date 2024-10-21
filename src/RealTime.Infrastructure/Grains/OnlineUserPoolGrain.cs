using RealTime.Domain.Interfaces;

namespace RealTime.Infrastructure.Grains;

public class OnlineUserPoolGrain : Grain, IOnlineUserPoolGrain
{
  private readonly Dictionary<string, IUserGrain> _onlineUsers = new();

  public async Task AddUser(string userId, string username, string? connectionId)
  {
    var userGrain = GrainFactory.GetGrain<IUserGrain>(userId);
    await userGrain.SetUsername(username);
    await userGrain.SetConnectionId(connectionId);
    _onlineUsers[userId] = userGrain;
  }

  public Task RemoveUser(string userId)
  {
    _onlineUsers.Remove(userId);
    return Task.CompletedTask;
  }

  public Task<List<IUserGrain>> GetAllUsers()
  {
    return Task.FromResult(_onlineUsers.Values.ToList());
  }
  
  public Task<IUserGrain?> GetUser(string userId)
  {
    _onlineUsers.TryGetValue(userId, out var userGrain);
    return Task.FromResult(userGrain);
  }
}