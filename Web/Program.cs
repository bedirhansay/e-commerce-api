using Infrastructure;
using Application;
using Application.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Servis eklemeleri
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HTTPS yönlendirme yapılandırması
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 5001; // HTTPS portu
});

// Yapılandırma dosyaları
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Custom servis eklemeleri
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplication(); // Artık AddCustomMapper burada dahil

// CORS yapılandırması
builder.Services.AddCors(options =>
{
    options.AddPolicy("SwaggerPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:5016") // Swagger UI'nin URL'si
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
// Uygulama oluşturma
var app = builder.Build();

// HTTP pipeline yapılandırması
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ExceptionMiddleware'i kullan
app.ConfigureExceptionHandler();

// // CORS politikasını etkinleştir
// app.UseCors("AllowAllOrigins");
//
// // HTTPS yönlendirme
// app.UseHttpsRedirection();

// Controller'ları haritala
app.MapControllers();

app.Run();