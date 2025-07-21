using Application.Features.Categories.Queries.GetOne;
using Application.Interfaces;
using Application.Models.Common;
using Application.Models;
using AutoMapper;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Features.Categories.Queries.GetList
{
    public class GetCategoryListQueryHandler(
        ICategoryRepositoy categoryRepository,
        IMapper mapper,
        ICurrentUserService currentUserService) : IRequestHandler<GetCategoryListQuery, PaginatedResult<CategoryViewModel>>
    {
        private readonly ICategoryRepositoy _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<PaginatedResult<CategoryViewModel>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Category, bool>> predicate = c =>
                (string.IsNullOrEmpty(request.SearchTerm) || c.Name.ToLower().Contains(request.SearchTerm.ToLower())) &&
                (_currentUserService.Role == Role.SuperAdmin.ToString() || c.UserId == _currentUserService.UserId);

            Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = request.SortBy?.ToLower() switch
            {
                "name" => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(c => c.Name)
                    : q => q.OrderByDescending(c => c.Name),

                "createdat" => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(c => c.CreatedAt)
                    : q => q.OrderByDescending(c => c.CreatedAt),

                "updatedat" => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(c => c.UpdatedAt)
                    : q => q.OrderByDescending(c => c.UpdatedAt),

                _ => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(c => c.CreatedAt)
                    : q => q.OrderByDescending(c => c.CreatedAt),
            };

            var (categories, totalItems) = await _categoryRepository.GetPaginatedAsync(
                predicate: predicate,
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                orderBy: orderBy
            );

            if (categories[0] != null && _currentUserService.UserId != categories[0].UserId && _currentUserService.Role != Role.SuperAdmin.ToString())
                return PaginatedResult<CategoryViewModel>.Fail("You are not authorized to see this category.");

            var viewModels = _mapper.Map<List<CategoryViewModel>>(categories);

            return PaginatedResult<CategoryViewModel>.Ok(viewModels, request.PageIndex, request.PageSize, totalItems);
        }
    }
}
