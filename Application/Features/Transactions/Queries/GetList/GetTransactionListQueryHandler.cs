using Application.Interfaces;
using Application.Models;
using Application.Models.Common;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System.Linq.Expressions;

namespace Application.Features.Transactions.Queries.GetList
{
    public class GetTransactionListQueryHandler(
        ITransactionRepository transactionRepository,
        IMapper mapper,
        ICurrentUserService currentUserService) : IRequestHandler<GetTransactionListQuery, PaginatedResult<TransactionViewModel>>
    {
        private readonly ITransactionRepository _transactionRepository = transactionRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<PaginatedResult<TransactionViewModel>> Handle(GetTransactionListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Transaction, bool>> predicate = t =>
                (string.IsNullOrEmpty(request.SearchTerm) || (t.Note != null && t.Note.ToLower().Contains(request.SearchTerm.ToLower()))) &&
                (!request.FromDate.HasValue || t.CreatedAt >= request.FromDate.Value) &&
                (!request.ToDate.HasValue || t.CreatedAt <= request.ToDate.Value) &&
                (!request.CategoryId.HasValue || t.CategoryId == request.CategoryId.Value) &&
                (!request.AmountMin.HasValue || t.Amount >= request.AmountMin.Value) &&
                (!request.AmountMax.HasValue || t.Amount <= request.AmountMax.Value);

            Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>>? orderBy = request.SortBy?.ToLower() switch
            {
                "amount" => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(t => t.Amount)
                    : q => q.OrderByDescending(t => t.Amount),

                "createdat" => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(t => t.CreatedAt)
                    : q => q.OrderByDescending(t => t.CreatedAt),

                "updatedat" => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(t => t.UpdatedAt)
                    : q => q.OrderByDescending(t => t.UpdatedAt),

                _ => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(t => t.CreatedAt)
                    : q => q.OrderByDescending(t => t.CreatedAt),
            };

            var (transactions, totalItems) = await _transactionRepository.GetPaginatedAsync(
                predicate: predicate,
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                orderBy: orderBy
            );

            if (transactions[0] != null && _currentUserService.UserId != transactions[0].UserId && _currentUserService.Role != Role.SuperAdmin.ToString())
                return PaginatedResult<TransactionViewModel>.Fail("You are not authorized to see this transactions.");

            var viewModels = _mapper.Map<List<TransactionViewModel>>(transactions);

            return PaginatedResult<TransactionViewModel>.Ok(viewModels, request.PageIndex, request.PageSize, totalItems);
        }
    }
}
