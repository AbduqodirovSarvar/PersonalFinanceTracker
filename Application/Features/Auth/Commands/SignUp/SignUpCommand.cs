using Application.Models;
using Application.Models.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.SignUp
{
    public record SignUpCommand(
        string UserName,
        string Password,
        string Email
        ) : IRequest<Result<UserViewModel>>;
}
