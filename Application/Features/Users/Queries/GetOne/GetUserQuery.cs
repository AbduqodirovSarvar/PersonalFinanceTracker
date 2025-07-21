using Application.Models;
using Application.Models.Common;
using MediatR;

namespace Application.Features.Users.Queries.GetOne
{
    public record GetUserQuery(
        Guid? UserId = null,
        string? UserName = null,
        string? Email = null) : IRequest<Result<UserViewModel>>;
}
