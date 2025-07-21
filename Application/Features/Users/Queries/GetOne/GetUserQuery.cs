using Application.Models;
using Application.Models.Common;
using Application.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetOne
{
    public record GetUserQuery(
        Guid? UserId = null,
        string? UserName = null,
        string? Email = null) : IRequest<Result<UserViewModel>>;
}
