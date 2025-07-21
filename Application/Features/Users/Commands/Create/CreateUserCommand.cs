using Application.Models;
using Application.Models.Common;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Create
{
    public record CreateUserCommand(
        string UserName,
        string Email,
        string Password,
        Role Role = Role.None
    ) : IRequest<Result<UserViewModel>>;
}
