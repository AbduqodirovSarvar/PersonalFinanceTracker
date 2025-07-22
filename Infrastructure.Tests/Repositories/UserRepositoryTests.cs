using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Application.Interfaces; // kerak bo‘ladi
using System;
using System.Threading.Tasks;

namespace Infrastructure.Tests.Repositories
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task GetByEmailAsync_ShouldReturnUser_WhenExists()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var mockCurrentUserService = new Mock<ICurrentUserService>();
            mockCurrentUserService.Setup(x => x.UserId).Returns(Guid.Parse("11111111-1111-1111-1111-111111111111"));

            var interceptor = new AuditableEntitySaveChangesInterceptor(mockCurrentUserService.Object);

            await using var context = new AppDbContext(options, interceptor);

            var user = new User
            {
                UserName = "testUser",
                Email = "test@mail.com",
                PasswordHash = "securehash"
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var repo = new UserRepository(context, null!);

            var result = await repo.GetAsync(x => x.Email == "test@mail.com");

            Assert.NotNull(result);
            Assert.Equal("test@mail.com", result!.Email);
        }
    }
}
