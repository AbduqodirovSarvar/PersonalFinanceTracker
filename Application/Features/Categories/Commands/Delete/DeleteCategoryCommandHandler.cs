using Application.Interfaces;
using Application.Models;
using Application.Models.Common;
using Domain.Enums;
using MediatR;

namespace Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommandHandler(
        ICategoryRepositoy categoryRepositoy,
        ICurrentUserService currentUserService)
        : IRequestHandler<DeleteCategoryCommand, Result<CategoryViewModel>>
    {
        private readonly ICategoryRepositoy _categoryRepository = categoryRepositoy;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<CategoryViewModel>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetAsync(c => c.Id == request.Id);

            if (category != null && _currentUserService.UserId != category.UserId && _currentUserService.Role != Role.SuperAdmin.ToString())
                return Result<CategoryViewModel>.Fail("You are not authorized to delete this category.");

            if (category is null) return Result<CategoryViewModel>.Fail("Category not found");

            await _categoryRepository.UpdateAsync(category);

            return Result<CategoryViewModel>.Ok("Category successfully deleted.");
        }
    }
}
