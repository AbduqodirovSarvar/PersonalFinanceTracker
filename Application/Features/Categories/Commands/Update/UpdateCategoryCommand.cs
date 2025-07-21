using Application.Models;
using Application.Models.Common;
using MediatR;

namespace Application.Features.Categories.Commands.Update
{
    public record UpdateCategoryCommand(Guid Id, string Name, string Color) : IRequest<Result<CategoryViewModel>>;
}
