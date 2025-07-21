using Application.Models;
using Application.Models.Common;
using Application.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetList
{
    public record GetUserListQuery(
        string? SearchTerm = null
        ) : BasePaginationQuery, IRequest<PaginatedResult<UserViewModel>>;
}
