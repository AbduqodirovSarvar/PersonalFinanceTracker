using Application.Models;
using Application.Models.Common;
using MediatR;

namespace Application.Features.Auth.Commands.SignUp
{
    public record SignUpCommand(
        string UserName,
        string Password,
        string Email
        ) : IRequest<Result<UserViewModel>>;
}
