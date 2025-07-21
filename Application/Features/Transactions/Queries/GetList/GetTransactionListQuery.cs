using Application.Models;
using Application.Models.Common;
using Application.Models.Queries;
using MediatR;

namespace Application.Features.Transactions.Queries.GetList
{
    public record GetTransactionListQuery(
        string? SearchTerm = null,
        DateTime? FromDate = null,
        DateTime? ToDate = null,
        Guid? CategoryId = null,
        decimal? AmountMin = null,
        decimal? AmountMax = null
        ) : BasePaginationQuery, IRequest<PaginatedResult<TransactionViewModel>>;
}
