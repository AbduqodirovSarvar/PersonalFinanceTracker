using Application.Models;
using Application.Models.Common;
using MediatR;

namespace Application.Features.Auth.Commands.SignIn
{
    public record SignInCommand(
        string Login,
        string Password
        ) : IRequest<Result<UserViewModel>>;
}
