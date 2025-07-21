using Application.Models;
using Application.Models.Common;
using Application.Models.Queries;
using MediatR;

namespace Application.Features.Users.Queries.GetList
{
    public record GetUserListQuery(
        string? SearchTerm = null
        ) : BasePaginationQuery, IRequest<PaginatedResult<UserViewModel>>;
}
