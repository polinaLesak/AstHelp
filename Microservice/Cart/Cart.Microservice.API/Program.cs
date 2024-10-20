using Cart.Microservice.API.Middleware;
using Cart.Microservice.Application.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.RegisterRequestHandlers();
builder.Services.AddJWTSwagger();
builder.Services.AddJWTAuthorization(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
