using System.Security.Cryptography;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RealTime.Domain.Constants;

var host = new HostBuilder()
  .ConfigureHostConfiguration(configHost =>
    configHost.AddEnvironmentVariables("ASPNETCORE_")
  )
  .ConfigureAppConfiguration((hostingContext, config) =>
  {
    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
    config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true);
    config.AddEnvironmentVariables();
    Console.WriteLine($"{hostingContext.HostingEnvironment.EnvironmentName}");
  })
  .ConfigureLogging((hostBuilderContext, loggingBuilder) =>
  {
    loggingBuilder.AddConfiguration(hostBuilderContext.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
  });

var userId = RandomNumberGenerator.GetHexString(10);
HubConnection? connectionBuilder;
await ConnectToSignalR();
await host.RunConsoleAsync();

Console.ReadLine();
return;

async Task ConnectToSignalR()
{
  var userConnectionUrl = $"http://localhost:5031/chatHub?userId={userId}";
  connectionBuilder = new HubConnectionBuilder()
    .WithUrl(userConnectionUrl)
    .Build();

  // connectionBuilder.On<string, string>(HubMethods.ReceiveMessage,
  //   (user, message) => { Console.WriteLine($"{user}: {message}"); });
  connectionBuilder.On<string>(HubMethods.ReceiveMessage, (message) =>
  {
    Console.WriteLine($"Received message: {message}");
  });

  await connectionBuilder.StartAsync();
  Console.WriteLine($"Connected to the hub. Listening for messages to UserId: {userId}");
}