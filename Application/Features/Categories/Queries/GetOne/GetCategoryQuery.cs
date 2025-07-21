using Application.Models;
using Application.Models.Common;
using MediatR;

namespace Application.Features.Categories.Queries.GetOne
{
    public record GetCategoryQuery(Guid Id) : IRequest<Result<CategoryViewModel>>;
}
