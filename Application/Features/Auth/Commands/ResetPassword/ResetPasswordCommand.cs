using Application.Models;
using Application.Models.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.ResetPassword
{
    public record ResetPasswordCommand(
        Guid UserId,
        string NewPassword,
        string ConformPassword,
        int ConfirmationCode = 12345
        ) : IRequest<Result<UserViewModel>>;
}
