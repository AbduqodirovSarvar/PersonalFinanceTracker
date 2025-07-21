using Application.Models;
using Application.Models.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.SignIn
{
    public record SignInCommand(
        string Login,
        string Password
        ) : IRequest<Result<UserViewModel>>;
}
