using Application.Models;
using Application.Models.Common;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Commands.ChangeRole
{
    public record ChangeRoleCommand(
        Guid UserId,
        Role NewRole
        ) : IRequest<Result<UserViewModel>>;
}
