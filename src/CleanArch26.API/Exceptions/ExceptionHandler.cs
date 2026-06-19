using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace CleanArch26.API.Exceptions;
public sealed class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";

        // FluentValidation.ValidationException için özel durum
        if (exception is ValidationException validationException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

            var validationErrors = validationException.Errors.Select(e => new
            {
                PropertyName = e.PropertyName,
                ErrorMessage = e.ErrorMessage
            }).ToList();

            var errorResult = new
            {
                StatusCode = 403,
                Errors = validationErrors
            };

            await httpContext.Response.WriteAsJsonAsync(errorResult, cancellationToken: cancellationToken);
            return true;
        }

        // Genel hata işleme
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var generalErrorResult = new
        {
            StatusCode = 500,
            Message = exception.Message,
            StackTrace = exception.StackTrace,
            Type = exception.GetType().Name,
            Source = exception.Source,
            InnerException = exception.InnerException?.Message
        };

        await httpContext.Response.WriteAsJsonAsync(generalErrorResult, cancellationToken: cancellationToken);
        return true;
    }
}