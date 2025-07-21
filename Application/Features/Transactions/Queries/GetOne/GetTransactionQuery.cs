using Application.Models.Common;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Transactions.Queries.GetOne
{
    public record GetTransactionQuery(Guid Id) : IRequest<Result<TransactionViewModel>>;
}
