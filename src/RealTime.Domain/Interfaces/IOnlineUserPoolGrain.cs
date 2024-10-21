namespace RealTime.Domain.Interfaces;

public interface IOnlineUserPoolGrain : IGrainWithStringKey
{
  Task AddUser(string userId, string username, string? connectionId);
  Task RemoveUser(string userId);
  Task<List<IUserGrain>> GetAllUsers();
  Task<IUserGrain?> GetUser(string userId);
}