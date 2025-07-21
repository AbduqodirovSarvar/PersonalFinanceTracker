using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.IntegrationTests.Controllers
{
    public class UserControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UserControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [TestMethod]
        public async Task GetUser_ReturnsOk_WhenUserExists()
        {
            var response = await _client.GetAsync("/api/users/some-user-id");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Assert.Contains("email", json.ToLower());
        }
    }
}
