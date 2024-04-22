using FluentValidation;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;

namespace OnionAPI.Application.Exceptions;

public class ExceptionMiddleware : IMiddleware
{   // sonraki adıma geçmeden exception yakalama
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
    }
    // exception içeriğinin düzenlenmesi
    private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        int statusCode = GetStatusCode(exception);
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;

        if(exception.GetType() == typeof(ValidationException)) // FluentValidationdan dönen exceptionları yakalarız
        {
            return httpContext.Response.WriteAsync(new ExceptionModel
            {
                Errors = ((ValidationException)exception).Errors.Select(x=> x.ErrorMessage),
                StatusCode = StatusCodes.Status400BadRequest,
            }.ToString());
        }

        List<string> errors =
        [
            $"Hata Mesajı = {exception.Message}",
            $"Hata Açıklaması = {exception.InnerException?.ToString()}",
        ];

        return httpContext.Response.WriteAsync(new ExceptionModel
        {
            Errors = errors,
            StatusCode = statusCode,
        }.ToString());
    }
    // exceptionları status codelara göre ayırma
    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError,
        };

}
