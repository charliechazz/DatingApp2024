using System.Net;
using System.Text;
using API.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Moq;

namespace API.Tests;

public class ExceptionMiddlewareTests
{
    private readonly Mock<RequestDelegate> _nextMock;
    private readonly Mock<ILogger<ExceptionMiddleware>> _loggerMock;
    private readonly Mock<IHostEnvironment> _envMock;

    public ExceptionMiddlewareTests()
    {
        _nextMock = new Mock<RequestDelegate>();
        _loggerMock = new Mock<ILogger<ExceptionMiddleware>>();
        _envMock = new Mock<IHostEnvironment>();
    }

    [Fact]
    public async Task InvokeAsync_ShouldHandleException_WhenDevelopment()
    {
        // Arrange: Simula un entorno de desarrollo
        _envMock.Setup(e => e.IsDevelopment()).Returns(true);
        var middleware = new ExceptionMiddleware(_nextMock.Object, _loggerMock.Object, _envMock.Object);

        // Simula que se lanza una excepción
        _nextMock.Setup(m => m(It.IsAny<HttpContext>())).Throws(new Exception("Test exception"));

        var context = new DefaultHttpContext();

        // Captura la respuesta en un MemoryStream
        var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        // Act: Ejecuta el middleware con una excepción
        await middleware.InvokeAsync(context);

        // Asegúrate de que el contenido se ha escrito en el MemoryStream
        memoryStream.Seek(0, SeekOrigin.Begin);
        using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
        {
            var responseBody = await reader.ReadToEndAsync();

            // Assert: Verifica que la respuesta contenga el StackTrace (solo en Desarrollo)
            Assert.Contains("stack trace", responseBody);
            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        }
    }

    [Fact]
    public async Task InvokeAsync_ShouldHandleException_WhenNotDevelopment()
    {
        // Arrange: Simula un entorno no de desarrollo
        _envMock.Setup(e => e.IsDevelopment()).Returns(false);
        var middleware = new ExceptionMiddleware(_nextMock.Object, _loggerMock.Object, _envMock.Object);

        // Simula que se lanza una excepción
        _nextMock.Setup(m => m(It.IsAny<HttpContext>())).Throws(new Exception("Test exception"));

        var context = new DefaultHttpContext();

        // Captura la respuesta en un MemoryStream
        var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        // Act: Ejecuta el middleware con una excepción
        await middleware.InvokeAsync(context);

        // Asegúrate de que el contenido se ha escrito en el MemoryStream
        memoryStream.Seek(0, SeekOrigin.Begin);
        using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
        {
            var responseBody = await reader.ReadToEndAsync();

            // Assert: Verifica que la respuesta no contenga el StackTrace (no en producción)
            Assert.DoesNotContain("stack trace", responseBody);
            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        }
    }
}