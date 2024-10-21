using RealTime.Domain.Constants;
using RealTime.Domain.Interfaces;

namespace RealTime.Application.Services.User;

public class UserService : IUserService
{
  private readonly IGrainFactory _grainFactory;

  public UserService(IGrainFactory grainFactory)
  {
    _grainFactory = grainFactory;
  }

  public async Task AddUserToOnlineUsers(string userId, string username, string connectionId)
  {
    var userGrain = _grainFactory.GetGrain<IUserGrain>(userId);
    await userGrain.SetUsername(username);
    await userGrain.SetConnectionId(connectionId);

    var poolGrain = _grainFactory.GetGrain<IOnlineUserPoolGrain>(OrleansConstants.OnlineUserPoolGrainKey);
    await poolGrain.AddUser(userId, username, connectionId);
  }

  public async Task RemoveUserFromOnlineUsers(string userId)
  {
    var userGrain = _grainFactory.GetGrain<IUserGrain>(userId);
    await userGrain.SetConnectionId(null);
    
    var poolGrain = _grainFactory.GetGrain<IOnlineUserPoolGrain>(OrleansConstants.OnlineUserPoolGrainKey);
    await poolGrain.RemoveUser(userId);
  }
  
  public async Task<List<string>> GetOnlineUsers()
  {
    var poolGrain = _grainFactory.GetGrain<IOnlineUserPoolGrain>(OrleansConstants.OnlineUserPoolGrainKey);
    var users = await poolGrain.GetAllUsers();
    return users.Select(u => u.GetPrimaryKeyString()).ToList();
  }

  public async Task<string> GetUsername(string userId)
  {
    var userGrain = _grainFactory.GetGrain<IUserGrain>(userId);
    return await userGrain.GetUsername();
  }
  
  public async Task<string?> GetConnectionIdFromUserPool(string userId)
  {
    var poolGrain = _grainFactory.GetGrain<IOnlineUserPoolGrain>(OrleansConstants.OnlineUserPoolGrainKey);
    var userGrain = await poolGrain.GetUser(userId);
    return userGrain != null ? await userGrain.GetConnectionId() : null;
  }
  
  public Task<IUserGrain> GetUser(string userId)
  {
    return Task.FromResult(_grainFactory.GetGrain<IUserGrain>(userId));
  }
  
}