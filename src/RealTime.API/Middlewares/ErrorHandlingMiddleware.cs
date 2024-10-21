using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RealTime.API.Middlewares;

public static class ErrorHandlingMiddleware
{
  public static void UseGlobalErrorHandler(this IApplicationBuilder app)
  {
    app.UseExceptionHandler(errorApp =>
    {
      errorApp.Run(async context =>
      {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature?.Error is not null)
        {
          var problemDetails = new ProblemDetails
          {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An unexpected error occurred!",
            Detail = exceptionHandlerPathFeature.Error.Message,
          };

          await context.Response.WriteAsJsonAsync(problemDetails);
        }
      });
    });
  }
}