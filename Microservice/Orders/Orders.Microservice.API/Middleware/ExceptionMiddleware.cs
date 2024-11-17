using Orders.Microservice.Application.Exceptions;

namespace Orders.Microservice.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); // передать управление следующему middleware
            }
            catch (Exception ex)
            {
                _logger.LogError($"Что-то пошло не так: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = context.Response;

            // Установить HTTP статус-код по типу исключения
            response.StatusCode = exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                FileNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var errorResponse = new
            {
                StatusCode = response.StatusCode,
                Message = exception.Message,
                Timestamp = DateTime.UtcNow
            };

            return context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
