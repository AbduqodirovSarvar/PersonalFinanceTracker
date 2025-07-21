using Application.Models;
using Application.Models.Common;
using MediatR;

namespace Application.Features.Transactions.Queries.GetOne
{
    public record GetTransactionQuery(Guid Id) : IRequest<Result<TransactionViewModel>>;
}
