using Orders.Microservice.API.Configuration;
using Orders.Microservice.API.Middleware;
using Orders.Microservice.Application.DI;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpClients(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.RegisterMediatrHandlers();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureJwtAuthorization(builder.Configuration);
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
