using Application.Models;
using Application.Models.Common;
using MediatR;

namespace Application.Features.Categories.Commands.Create
{
    public record CreateCategoryCommand(
        string Name,
        string Color) : IRequest<Result<CategoryViewModel>>;
}
