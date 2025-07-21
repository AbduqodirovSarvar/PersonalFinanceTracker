using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests.Repositories
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task GetByEmailAsync_ShouldReturnUser_WhenExists()
        {
            var user = new User { UserName = "testUser", Email = "test@mail.com", PasswordHash = "ggdjbqwue346783gf734x2389389ybg2d38h921yu" };
            var users = new List<User> { user }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var repo = new UserRepository(mockContext.Object, null!);

            var result = await repo.GetAsync(x => x.Email == "test@mail.com");

            Assert.NotNull(result);
            Assert.Equal("test@mail.com", result.Email);
        }
    }
}
