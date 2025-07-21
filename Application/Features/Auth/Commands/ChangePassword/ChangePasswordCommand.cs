using Application.Models;
using Application.Models.Common;
using MediatR;

namespace Application.Features.Auth.Commands.ChangePassword
{
    public record ChangePasswordCommand(
        Guid UserId,
        string OldPassword,
        string NewPassword
        ) : IRequest<Result<UserViewModel>>;
}
