using Application.Features.Users.Queries.GetOne;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Features.Users
{
    public class GetUserQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnUser_WhenUserExists()
        {
            var user = new User { Id = Guid.NewGuid(), Email = "test@mail.com" };

            var mockRepo = new Mock<IUserRepositoy>();
            mockRepo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                    .ReturnsAsync(user);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<UserViewModel>(It.IsAny<User>()))
                      .Returns(new UserViewModel { Email = user.Email });

            var handler = new GetUserQueryHandler(mockRepo.Object, mockMapper.Object);
            var result = await handler.Handle(new GetUserQuery(UserId: user.Id), default);

            Assert.True(result.Success);
            Assert.Equal("test@mail.com", result?.Data?.Email);
        }
    }
}
