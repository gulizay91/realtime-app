namespace RealTime.Domain.Interfaces;

public interface IUserGrain : IGrainWithStringKey
{
  Task SetUsername(string username);
  Task<string> GetUsername();
  Task SetConnectionId(string? connectionId);
  Task<string?> GetConnectionId();
}