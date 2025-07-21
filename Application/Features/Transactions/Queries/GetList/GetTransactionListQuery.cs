using Application.Models.Common;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Queries;

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
