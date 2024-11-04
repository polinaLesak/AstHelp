using Cart.Microservice.API.Configuration;
using Cart.Microservice.API.Middleware;
using Cart.Microservice.Application.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.RegisterMediatrHandlers();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureJwtAuthorization(builder.Configuration);
builder.Services.ConfigureRabbitMq(builder.Configuration);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
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
