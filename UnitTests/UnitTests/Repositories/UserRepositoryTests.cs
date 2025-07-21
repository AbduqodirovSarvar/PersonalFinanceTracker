using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.UnitTests.Repositories
{
    public class UserRepositoryTests
    {
        [TestMethod]
        public async Task GetByEmailAsync_ShouldReturnUser_WhenExists()
        {
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<AppDbContext>();
            var user = new User { Email = "test@mail.com" };

            var data = new List<User> { user }.AsQueryable();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var repo = new UserRepository(mockContext.Object, null!);
            var result = await repo.GetByEmailAsync("test@mail.com");

            Assert.Equal("test@mail.com", result?.Email);
        }
    }
}
