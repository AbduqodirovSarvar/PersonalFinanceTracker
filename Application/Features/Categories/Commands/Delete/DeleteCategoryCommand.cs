using Application.Models;
using Application.Models.Common;
using MediatR;

namespace Application.Features.Categories.Commands.Delete
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<Result<CategoryViewModel>>;
}
