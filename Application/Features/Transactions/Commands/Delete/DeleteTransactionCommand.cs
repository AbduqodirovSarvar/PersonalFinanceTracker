using Application.Models;
using Application.Models.Common;
using MediatR;

namespace Application.Features.Transactions.Commands.Delete
{
    public record DeleteTransactionCommand(Guid Id) : IRequest<Result<TransactionViewModel>>;
}
