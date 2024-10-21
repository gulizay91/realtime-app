namespace RealTime.Application.Interfaces.Hubs;

public interface IChatHub
{
  Task SendMessageToUser(string userId, string message, CancellationToken cancellationToken);
  Task SendMessageToAllOnlineUsers(string message, CancellationToken cancellationToken);
  Task SendMessageToAllUsers(string message, CancellationToken cancellationToken);
}