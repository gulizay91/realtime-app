using FastEndpoints;
using RealTime.API.Middlewares;
using RealTime.API.Registrations;
using RealTime.Application.Registrations;
using RealTime.Infrastructure.Registrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddHealthChecks();

builder.Services.RegisterSettings(builder.Configuration);
builder.Services.RegisterLoggers(builder.Configuration);
builder.Services.RegisterSwagger();
builder.Services.RegisterHttpClients(builder.Configuration);
builder.Services.RegisterServices();
builder.Services.RegisterApplicationServices();
builder.Services.RegisterSignalRHub();
builder.Services.RegisterMediatr();
builder.Services.RegisterOrleansClient();

var app = builder.Build();

app.MapGet("/", () => "RealTime API is running.");


// Error handling middleware
app.UseGlobalErrorHandler();
app.UseRouting();

app.UseSwagger();
app.UseFastEndpoints(c =>
{
  c.Endpoints.RoutePrefix = "api";
  c.Versioning.Prefix = "v";
  c.Versioning.PrependToRoute = true;
});

app.UseHealthCheckEndpoints();
app.UseSignalRHub();

app.Urls.Add("http://*:5031");

app.Run();