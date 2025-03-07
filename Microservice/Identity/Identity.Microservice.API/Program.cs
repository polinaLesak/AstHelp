using Identity.Microservice.API.Configuration;
using Identity.Microservice.API.Middleware;
using Identity.Microservice.Application.DI;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.RegisterRequestHandlers();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureAuthorization(builder.Configuration);
builder.Services.ConfigureRabbitMq(builder.Configuration);
builder.Services.ConfigureCors(MyAllowSpecificOrigins);

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
