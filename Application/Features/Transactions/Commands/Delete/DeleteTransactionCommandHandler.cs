using Application.Interfaces;
using Application.Models;
using Application.Models.Common;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Transactions.Commands.Delete
{
    public class DeleteTransactionCommandHandler(
        ITransactionRepository transactionRepository,
        ICurrentUserService currentUserService) : IRequestHandler<DeleteTransactionCommand, Result<TransactionViewModel>>
    {
        private readonly ITransactionRepository _transactionRepository = transactionRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<TransactionViewModel>> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetAsync(x => x.Id == request.Id);

            if (transaction != null && _currentUserService.UserId != transaction.UserId && _currentUserService.Role != Role.SuperAdmin.ToString())
                return Result<TransactionViewModel>.Fail("You are not authorized to delete this transaction.");

            if (transaction == null) return Result<TransactionViewModel>.Fail("Transaction not found.");

            await _transactionRepository.DeleteAsync(transaction);

            return Result<TransactionViewModel>.Ok("Transaction deleted.");
        }
    }
}
