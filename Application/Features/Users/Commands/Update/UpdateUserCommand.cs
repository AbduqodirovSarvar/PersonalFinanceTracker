using Application.Models;
using Application.Models.Common;
using MediatR;

namespace Application.Features.Users.Commands.Update
{
    public record UpdateUserCommand(
        Guid Id,
        string Email,
        string UserName) : IRequest<Result<UserViewModel>>;
}
