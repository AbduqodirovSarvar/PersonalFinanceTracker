using Application.Interfaces;
using Application.Models.Common;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetList
{
    public class GetUserListQueryHandler(IUserRepositoy userRepository, IMapper mapper) : IRequestHandler<GetUserListQuery, PaginatedResult<UserViewModel>>
    {
        private readonly IUserRepositoy _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<PaginatedResult<UserViewModel>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<User, bool>> predicate = u =>
                (string.IsNullOrEmpty(request.SearchTerm) ||
                 u.UserName.Contains(request.SearchTerm) ||
                 u.Email.Contains(request.SearchTerm));

            Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = request.SortBy?.ToLower() switch
            {
                "id" => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(u => u.Id)
                    : q => q.OrderByDescending(u => u.Id),

                "username" => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(u => u.UserName)
                    : q => q.OrderByDescending(u => u.UserName),

                "email" => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(u => u.Email)
                    : q => q.OrderByDescending(u => u.Email),

                "createdat" => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(u => u.CreatedAt)
                    : q => q.OrderByDescending(u => u.CreatedAt),

                "updatedat" => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(u => u.UpdatedAt)
                    : q => q.OrderByDescending(u => u.UpdatedAt),

                _ => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(u => u.CreatedAt)
                    : q => q.OrderByDescending(u => u.CreatedAt),
            };

            var (users, totalItems) = await _userRepository.GetPaginatedAsync(
                predicate: predicate,
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                orderBy: orderBy
            );

            var viewModels = _mapper.Map<List<UserViewModel>>(users);

            return PaginatedResult<UserViewModel>.Ok(viewModels, request.PageIndex, request.PageSize, totalItems);
        }
    }
}
