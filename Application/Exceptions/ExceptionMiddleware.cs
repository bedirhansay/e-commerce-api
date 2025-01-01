using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;
using System.Net;
using System.Text.Json;

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
    
        // Hata mesajlarını bir liste halinde topluyoruz
        List<string> errors = new()
        {
            exception.Message // Hatanın ana mesajı
        };

        // Eğer bir iç hata (InnerException) varsa, bunu da ekliyoruz
        if (exception.InnerException != null)
        {
            errors.Add(exception.InnerException.Message);
        }

        // ExceptionModel nesnesini oluşturuyoruz
        var errorResponse = new ExceptionModel
        {
            StatusCode = statusCode,
            Errors = errors // Hata mesajlarını liste olarak ekle
        };

        // Oluşturulan ExceptionModel nesnesini JSON formatında döndür
        return httpContext.Response.WriteAsync(errorResponse.ToString());
    }

    private static int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest, // Hatalı İstek
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized, // Yetkisiz
            ValidationException => StatusCodes.Status400BadRequest, // Doğrulama Hatası
            NotFoundException => StatusCodes.Status404NotFound, // Kaynak Bulunamadı
            ForbiddenException => StatusCodes.Status403Forbidden, // Yasaklı Erişim
            TooManyRequestsException => StatusCodes.Status429TooManyRequests, // Çok Fazla İstek
            TimeoutException => StatusCodes.Status408RequestTimeout, // Zaman Aşımı
            _ => StatusCodes.Status500InternalServerError // Diğer hatalar
        };
    }
}