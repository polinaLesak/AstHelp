using Identity.Microservice.Application.Commands;
using Identity.Microservice.Application.DTOs.Response;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace Identity.Microservice.Tests.Integration
{
    public class AuthIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AuthIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Register_ShouldReturnOk_WhenRegistrationIsSuccessful()
        {
            var registerDto = new RegisterUserCommand { Username = "testUser", Email = "test@example.com", Password = "password" };

            var response = await _client.PostAsJsonAsync("/api/auth/register", registerDto);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal("true", result);
        }

        [Fact]
        public async Task Login_ShouldReturnToken_WhenLoginIsSuccessful()
        {
            var loginDto = new LoginUserCommand("testUser", "password");

            var response = await _client.PostAsJsonAsync("/api/auth/login", loginDto);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<UserLoginResponseDto>();
            Assert.NotNull(result.JwtToken);
        }
    }

}
