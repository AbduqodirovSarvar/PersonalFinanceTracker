using Application.Interfaces;
using Application.Models;
using Application.Models.Common;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.Features.Categories.Queries.GetOne
{
    public class GetCategoryQueryHandler(
        ICategoryRepositoy categoryRepositoy,
        IMapper mapper,
        ICurrentUserService currentUserService)
        : IRequestHandler<GetCategoryQuery, Result<CategoryViewModel>>
    {
        private readonly ICategoryRepositoy _categoryRepository = categoryRepositoy;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<CategoryViewModel>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetAsync(c => c.Id == request.Id);

            if (category != null && _currentUserService.UserId != category.UserId && _currentUserService.Role != Role.SuperAdmin.ToString())
                return Result<CategoryViewModel>.Fail("You are not authorized to see this category.");

            if (category is null) return Result<CategoryViewModel>.Fail("Category not found");

            return Result<CategoryViewModel>.Ok(_mapper.Map<CategoryViewModel>(category));
        }
    }
}
