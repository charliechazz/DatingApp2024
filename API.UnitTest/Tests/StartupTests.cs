using Microsoft.Extensions.Configuration;

namespace API.UnitTest.Tests
{
    public class StartupTests
    {
        private readonly IConfiguration _configuration;

        public StartupTests()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true); // El archivo es opcional
#pragma warning disable CS8620 // El argumento no se puede usar para el parámetro debido a las diferencias en la nulabilidad de los tipos de referencia.
            builder.AddInMemoryCollection(new Dictionary<string, string> { { "SomeKey", "SomeValue" } }); // Fallback en memoria
#pragma warning restore CS8620 // El argumento no se puede usar para el parámetro debido a las diferencias en la nulabilidad de los tipos de referencia.
            _configuration = builder.Build();
        }

        [Fact]
        public void Configuration_ShouldBeSetCorrectly()
        {
            // Arrange
            var startup = new Startup(_configuration);

            // Act & Assert
            Assert.NotNull(startup.Configuration); // Verifica que la propiedad Configuration no sea null

            // Verificar que un valor específico esté presente
            Assert.Equal("SomeValue", _configuration["SomeKey"]);
        }
    }
}