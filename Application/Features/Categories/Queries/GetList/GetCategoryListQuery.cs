using Application.Models;
using Application.Models.Common;
using Application.Models.Queries;
using MediatR;

namespace Application.Features.Categories.Queries.GetList
{
    public record GetCategoryListQuery(
        string? SearchTerm = null
        ) : BasePaginationQuery, IRequest<PaginatedResult<CategoryViewModel>>;
}
