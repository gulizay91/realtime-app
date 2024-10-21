using RealTime.Domain.Interfaces;

namespace RealTime.Application.Services.User;

public interface IUserService
{
  Task AddUserToOnlineUsers(string userId, string username, string connectionId);
  Task RemoveUserFromOnlineUsers(string userId);
  Task<List<string>> GetOnlineUsers();
  Task<string> GetUsername(string userId);

  Task<string?> GetConnectionIdFromUserPool(string userId);
  Task<IUserGrain> GetUser(string userId);
}