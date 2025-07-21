using Application.Models;
using Application.Models.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.ChangePassword
{
    public record ChangePasswordCommand(
        Guid UserId,
        string OldPassword,
        string NewPassword
        ) : IRequest<Result<UserViewModel>>;
}
