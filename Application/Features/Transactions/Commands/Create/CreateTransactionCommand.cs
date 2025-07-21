using Application.Models;
using Application.Models.Common;
using MediatR;

namespace Application.Features.Transactions.Commands.Create
{
    public record CreateTransactionCommand(
        decimal Amount,
        string Note,
        Guid CategoryId) : IRequest<Result<TransactionViewModel>>;
}
