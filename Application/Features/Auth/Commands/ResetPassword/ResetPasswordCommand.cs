using Application.Models;
using Application.Models.Common;
using MediatR;

namespace Application.Features.Auth.Commands.ResetPassword
{
    public record ResetPasswordCommand(
        Guid UserId,
        string NewPassword,
        string ConformPassword,
        int ConfirmationCode = 12345
        ) : IRequest<Result<UserViewModel>>;
}
