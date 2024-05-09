using FirstBackend.Core.Exсeptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FirstBackend.API.Configuration;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly Serilog.ILogger _logger = Log.ForContext<GlobalExceptionHandler>();

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.Error(
            exception, "Exception occurred: {Message}", exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = exception.GetType().Name,
            Title = "Server error",
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
        };

        switch (problemDetails.Type)
        {
            case nameof(ConflictException):
                {
                    problemDetails.Status = StatusCodes.Status409Conflict;
                    problemDetails.Title = "Конфликт";
                };

                break;

            case nameof(NotFoundException):
                {
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Title = "Не найдены данные по запросу";
                };

                break;

            case nameof(BadRequestException):
                {
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Некорректные входные данные";
                };

                break;

            case nameof(UnauthorizedException):
                {
                    problemDetails.Status = StatusCodes.Status401Unauthorized;
                    problemDetails.Title = "Неверные аутентификационные данные";
                };

                break;
        }


        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
