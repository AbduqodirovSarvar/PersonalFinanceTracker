using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.UnitTests.Features
{
    public class GetUserQueryHandlerTests
    {
        [TestMethod]
        public async Task Handle_ShouldReturnUser_WhenUserExists()
        {
            var user = new User { Id = Guid.NewGuid(), Email = "test@mail.com" };
            var mockRepo = new Mock<IUserRepositoy>();
            mockRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<UserViewModel>(It.IsAny<User>()))
                      .Returns(new UserViewModel { Email = user.Email });

            var handler = new GetUserQueryHandler(mockRepo.Object, mockMapper.Object);
            var result = await handler.Handle(new GetUserQuery(UserId: user.Id), default);

            Assert.True(result.Succeeded);
            Assert.Equal("test@mail.com", result.Data.Email);
        }
    }
}
