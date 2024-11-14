using API.Services;
using API.DataEntities;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.UnitTest.Tests
{
    public class TokenServiceTests
    {
        private readonly Mock<IConfiguration> _configMock;
        private readonly TokenService _tokenService;

        public TokenServiceTests()
        {
            // Mock de IConfiguration para simular la clave del token
            _configMock = new Mock<IConfiguration>();
            _configMock.Setup(config => config["TokenKey"]).Returns("ThisIsASecretKeyThatIsAtLeast64CharactersLongToPassValidationThisIsASecretKeyThatIsAtLeast64CharactersLongToPassValidationThisIsASecretKeyThatIsAtLeast64CharactersLongToPassValidationThisIsASecretKeyThatIsAtLeast64CharactersLongToPassValidationThisIsASecretKeyThatIsAtLeast64CharactersLongToPassValidationThisIsASecretKeyThatIsAtLeast64CharactersLongToPassValidationThisIsASecretKeyThatIsAtLeast64CharactersLongToPassValidationThisIsASecretKeyThatIsAtLeast64CharactersLongToPassValidation");

            _tokenService = new TokenService(_configMock.Object);
        }

        [Fact]
        public void CreateToken_ShouldGenerateToken_WhenValidAppUserIsProvided()
        {
            // Arrange
            var user = new AppUser
            {
                UserName = "testuser",
                KnownAs = "Test",
                Gender = "Male",
                City = "TestCity",
                Country = "TestCountry"
            };

            // Act
            var token = _tokenService.CreateToken(user);

            // Assert
            Assert.NotNull(token);
            var tokenHandler = new JwtSecurityTokenHandler();
            var parsedToken = tokenHandler.ReadJwtToken(token);

            // Validar que el token tiene el Claim esperado
            var usernameClaim = parsedToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            Assert.NotNull(usernameClaim); // Verifica que el claim de NameIdentifier exista
            Assert.Equal("testuser", usernameClaim.Value); // Asegúrate de que tenga el valor correcto
        }

        [Fact]
        public void CreateToken_ShouldThrowArgumentException_WhenTokenKeyIsShort()
        {
            // Arrange
            _configMock.Setup(config => config["TokenKey"]).Returns("ShortKey");

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _tokenService.CreateToken(new AppUser
            {
                UserName = "testuser",
                KnownAs = "Test",
                Gender = "Male",
                City = "TestCity",
                Country = "TestCountry"
            }));

            Assert.Equal("TokenKey too short", exception.Message);
        }

        [Fact]
        public void CreateToken_ShouldThrowArgumentException_WhenTokenKeyIsMissing()
        {
            // Arrange
#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
            _configMock.Setup(config => config["TokenKey"]).Returns<string>(null);
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _tokenService.CreateToken(new AppUser
            {
                UserName = "testuser",
                KnownAs = "Test",
                Gender = "Male",
                City = "TestCity",
                Country = "TestCountry"
            }));

            Assert.Equal("TokenKey not found", exception.Message);
        }
    }
}