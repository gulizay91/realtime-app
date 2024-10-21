using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RealTime.Application.Interfaces.Hubs;
using RealTime.Application.Services.User;
using RealTime.Domain.Constants;

namespace RealTime.Infrastructure.Hubs;

public class ChatHub : Hub, IChatHub
{
  private readonly ILogger<ChatHub> _logger;
  private readonly IUserService _userService;
  private readonly IHubContext<ChatHub> _hubContext;
  
  public ChatHub(ILogger<ChatHub> logger, IUserService userService, IHubContext<ChatHub> hubContext)
  {
    _logger = logger;
    _userService = userService;
    _hubContext = hubContext;
  }
  
  public override async Task OnConnectedAsync()
  {
    var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();
    var username = Context.GetHttpContext()?.Request.Query["username"].ToString();
    var connectionId = Context.ConnectionId;

    if (!string.IsNullOrWhiteSpace(userId))
    {
      await _userService.AddUserToOnlineUsers(userId, username, connectionId);
      _logger.LogInformation($"User connected with ID: {userId}, Username: {username}");
    }
    else
    {
      _logger.LogWarning("UserId is missing.");
    }
    
    await base.OnConnectedAsync();
  }

  public override async Task OnDisconnectedAsync(Exception? exception)
  {
    var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();
    if (!string.IsNullOrWhiteSpace(userId))
    {
      await _userService.RemoveUserFromOnlineUsers(userId);
      _logger.LogInformation($"User {userId} disconnected.");
    }

    await base.OnDisconnectedAsync(exception);
  }

  public async Task SendMessageToUser(string userId, string message, CancellationToken cancellationToken)
  {
    var connectionId = await _userService.GetConnectionIdFromUserPool(userId);
    if (!string.IsNullOrEmpty(connectionId))
    {
      await _hubContext.Clients.Client(connectionId).SendAsync(HubMethods.ReceiveMessage, message, cancellationToken: cancellationToken);
    }
    else
    {
      _logger.LogWarning($"No active connection found for UserId: {userId}");
    }
  }
  
  public async Task SendMessageToAllOnlineUsers(string message, CancellationToken cancellationToken)
  {
    var users = await _userService.GetOnlineUsers();
    foreach (var userId in users)
    {
      var connectionId = await _userService.GetConnectionIdFromUserPool(userId);
      if (!string.IsNullOrEmpty(connectionId))
      {
        await _hubContext.Clients.Client(connectionId).SendAsync(HubMethods.ReceiveMessage, message, cancellationToken);
      }
    }
  }
  
  public async Task SendMessageToAllUsers(string message, CancellationToken cancellationToken)
  {
    await _hubContext.Clients.All.SendAsync(HubMethods.ReceiveMessage, message, cancellationToken);
  }
}