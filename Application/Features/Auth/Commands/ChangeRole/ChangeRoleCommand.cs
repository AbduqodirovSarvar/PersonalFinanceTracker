using Application.Models;
using Application.Models.Common;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.ChangeRole
{
    public record ChangeRoleCommand(
        Guid UserId,
        Role NewRole
        ) : IRequest<Result<UserViewModel>>;
}
