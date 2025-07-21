using Application.Models.Common;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Transactions.Commands.Update
{
    public record UpdateTransactionCommand(
        Guid Id,
        decimal Amount,
        string Note,
        Guid CategoryId) : IRequest<Result<TransactionViewModel>>;
}
