using Application.Models;
using Application.Models.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Update
{
    public record UpdateUserCommand(
        Guid Id,
        string Email,
        string UserName) : IRequest<Result<UserViewModel>>;
}
