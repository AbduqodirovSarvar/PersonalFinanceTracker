using Application.Models;
using Application.Models.Common;
using Application.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Queries.GetList
{
    public record GetCategoryListQuery(
        string? SearchTerm = null
        ) : BasePaginationQuery, IRequest<PaginatedResult<CategoryViewModel>>;
}
