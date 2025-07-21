using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests.Controllers
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UserControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient(); // Real server URL bilan
        }

        [Fact]
        public async Task GetList_ShouldReturnSuccessStatusCode()
        {
            // Arrange
            var url = "/api/user";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            json.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Create_ShouldReturnOk_WhenValidUser()
        {
            // Arrange
            var url = "/api/user";

            var newUser = new
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "SecurePassword123!",
                Role = "User" // Role tipini loyihangizga qarab o‘zgartiring
            };

            // Act
            var response = await _client.PostAsJsonAsync(url, newUser);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_WithInvalidId_ShouldReturnNotFoundOrBadRequest()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var url = $"/api/user/{invalidId}";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.StatusCode.Should().BeOneOf(HttpStatusCode.NotFound, HttpStatusCode.BadRequest);
        }
    }
}
