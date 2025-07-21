using Application.Interfaces;
using Application.Models;
using Application.Models.Common;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommandHandler(
        ICategoryRepositoy categoryRepositoy,
        IMapper mapper,
        ICurrentUserService currentUserService)
        : IRequestHandler<UpdateCategoryCommand, Result<CategoryViewModel>>
    {
        private readonly ICategoryRepositoy _categoryRepository = categoryRepositoy;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<CategoryViewModel>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetAsync(c => c.Id == request.Id);

            if (category != null && _currentUserService.UserId != category.UserId && _currentUserService.Role != Role.SuperAdmin.ToString())
                return Result<CategoryViewModel>.Fail("You are not authorized to update this category.");

            if (category is null) return Result<CategoryViewModel>.Fail("Category not found");

            category.Name = request.Name;
            category.Color = request.Color;

            await _categoryRepository.UpdateAsync(category);

            return Result<CategoryViewModel>.Ok(_mapper.Map<CategoryViewModel>(category));
        }
    }
}
