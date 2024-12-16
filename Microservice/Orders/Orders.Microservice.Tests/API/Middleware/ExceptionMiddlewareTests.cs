using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Orders.Microservice.API.Middleware;
using Orders.Microservice.Application.Exceptions;

namespace Orders.Microservice.Tests.API.Middleware;

public class ExceptionMiddlewareTests
{
    private readonly Mock<RequestDelegate> _nextMock;
    private readonly Mock<ILogger<ExceptionMiddleware>> _loggerMock;
    private readonly ExceptionMiddleware _middleware;

    public ExceptionMiddlewareTests()
    {
        _nextMock = new Mock<RequestDelegate>();
        _loggerMock = new Mock<ILogger<ExceptionMiddleware>>();
        _middleware = new ExceptionMiddleware(_nextMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task InvokeAsync_ShouldCallNext_WhenNoException()
    {
        var context = new DefaultHttpContext();

        await _middleware.InvokeAsync(context);

        _nextMock.Verify(next => next(context), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_ShouldReturn404_WhenNotFoundException()
    {
        _nextMock.Setup(next => next(It.IsAny<HttpContext>())).ThrowsAsync(new NotFoundException("Not Found"));

        var context = new DefaultHttpContext();
        var responseStream = new MemoryStream();
        context.Response.Body = responseStream;

        await _middleware.InvokeAsync(context);

        Assert.Equal(StatusCodes.Status404NotFound, context.Response.StatusCode);
    }


    [Fact]
    public async Task InvokeAsync_ShouldReturn401_WhenUnauthorizedAccessException()
    {
        _nextMock.Setup(next => next(It.IsAny<HttpContext>())).ThrowsAsync(new UnauthorizedAccessException("Unauthorized"));

        var context = new DefaultHttpContext();
        var responseStream = new MemoryStream();
        context.Response.Body = responseStream;

        await _middleware.InvokeAsync(context);

        Assert.Equal(StatusCodes.Status401Unauthorized, context.Response.StatusCode);
    }

    [Fact]
    public async Task InvokeAsync_ShouldReturn500_WhenUnhandledException()
    {
        _nextMock.Setup(next => next(It.IsAny<HttpContext>())).ThrowsAsync(new Exception("Server Error"));

        var context = new DefaultHttpContext();
        var responseStream = new MemoryStream();
        context.Response.Body = responseStream;

        await _middleware.InvokeAsync(context);

        Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
    }

    [Fact]
    public async Task InvokeAsync_ShouldReturn404_WhenFileNotFoundException()
    {
        _nextMock.Setup(next => next(It.IsAny<HttpContext>())).ThrowsAsync(new FileNotFoundException("File Not Found"));

        var context = new DefaultHttpContext();
        var responseStream = new MemoryStream();
        context.Response.Body = responseStream;

        await _middleware.InvokeAsync(context);

        Assert.Equal(StatusCodes.Status404NotFound, context.Response.StatusCode);
    }
}