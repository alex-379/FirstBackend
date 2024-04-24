using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FirstBackend.API.Exceptions;

internal sealed class GlobalExceptionHandler : IExceptionHandler
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
            case "ValidationException":
                {
                    problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                    problemDetails.Title = "Ошибка валидации";
                };

                break;

            case "NotFoundException":
                {
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Title = "Не найдены данные по запросу";
                };

                break;
        }


        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
