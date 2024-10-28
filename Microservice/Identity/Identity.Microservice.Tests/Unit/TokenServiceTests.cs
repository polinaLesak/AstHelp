using Identity.Microservice.Application.Services;
using Identity.Microservice.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Identity.Microservice.Tests.Unit
{
    public class TokenServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly TokenService _tokenService;

        public TokenServiceTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(config => config["Jwt:Key"]).Returns("46be0927a4f86577f17ce6d10bc6aa61");
            _configurationMock.Setup(config => config["Jwt:Issuer"]).Returns("Issuer");
            _configurationMock.Setup(config => config["Jwt:Audience"]).Returns("Audience");

            _tokenService = new TokenService(_configurationMock.Object);
        }

        [Fact]
        public void GenerateToken_ShouldReturnToken_WhenUserIsValid()
        {
            var user = new User { Username = "testUser", Email = "test@example.com" };

            var token = _tokenService.GenerateToken(user);

            Assert.NotNull(token);
        }
    }

}
