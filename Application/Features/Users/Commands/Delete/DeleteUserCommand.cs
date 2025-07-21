using Application.Models;
using Application.Models.Common;
using MediatR;

namespace Application.Features.Users.Commands.Delete
{
    public record DeleteUserCommand(
        Guid Id
    ) : IRequest<Result<UserViewModel>>;
}
