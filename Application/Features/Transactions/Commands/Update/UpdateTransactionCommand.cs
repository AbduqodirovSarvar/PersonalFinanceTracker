using Application.Models;
using Application.Models.Common;
using MediatR;

namespace Application.Features.Transactions.Commands.Update
{
    public record UpdateTransactionCommand(
        Guid Id,
        decimal Amount,
        string Note,
        Guid CategoryId) : IRequest<Result<TransactionViewModel>>;
}
