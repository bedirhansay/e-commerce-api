using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;
using System.Net;
using System.Text.Json;
using FluentValidation;
using ValidationException = FluentValidation.ValidationException;

namespace Application.Exceptions;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json"; // Yanıt formatını JSON olarak ayarla
        int statusCode = GetStatusCode(exception); // Hata durum kodunu belirle
        httpContext.Response.StatusCode = statusCode; // HTTP yanıt durum kodunu ayarla
    
        List<string> errors = new();

        // Eğer FluentValidation'dan gelen bir doğrulama hatası varsa
        if (exception is ValidationException validationException)
        {
            errors.AddRange(validationException.Errors.Select(e => e.ErrorMessage)); // Tüm hata mesajlarını listeye ekle
        }
        else
        {
            // Diğer hatalar için standart mesaj
            errors.Add(exception.Message);

            if (exception.InnerException != null)
            {
                errors.Add(exception.InnerException.Message);
            }
        }

        // ExceptionModel nesnesini oluşturuyoruz
        var errorResponse = new ExceptionModel
        {
            StatusCode = statusCode,
            Errors = errors
        };

        // Oluşturulan ExceptionModel nesnesini JSON formatında döndür
        return httpContext.Response.WriteAsync(errorResponse.ToString());
    }

    private static int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            ValidationException => StatusCodes.Status400BadRequest, // Doğrulama hatası
            BadRequestException => StatusCodes.Status400BadRequest, // Hatalı İstek
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized, // Yetkisiz
            NotFoundException => StatusCodes.Status404NotFound, // Kaynak Bulunamadı
            ForbiddenException => StatusCodes.Status403Forbidden, // Yasaklı Erişim
            TooManyRequestsException => StatusCodes.Status429TooManyRequests, // Çok Fazla İstek
            TimeoutException => StatusCodes.Status408RequestTimeout, // Zaman Aşımı
            _ => StatusCodes.Status500InternalServerError // Diğer hatalar
        };
    }
}