using Application.Interfaces;
using Application.Models.Common;
using Application.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Transactions.Commands.Update
{
    public class UpdateTransactionCommandHandler(
        ITransactionRepository transactionRepository,
        IMapper mapper,
        ICurrentUserService currentUserService)
        : IRequestHandler<UpdateTransactionCommand, Result<TransactionViewModel>>
    {
        private readonly ITransactionRepository _transactionRepository = transactionRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<TransactionViewModel>> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetAsync(x => x.Id == request.Id);

            if (transaction != null && _currentUserService.UserId != transaction.UserId && _currentUserService.Role != Role.SuperAdmin.ToString())
                return Result<TransactionViewModel>.Fail("You are not authorized to update this transaction.");

            if (transaction == null) return Result<TransactionViewModel>.Fail("Transaction not found.");

            transaction.Amount = request.Amount;
            transaction.Note = request.Note;
            transaction.CategoryId = request.CategoryId;

            await _transactionRepository.UpdateAsync(transaction);

            return Result<TransactionViewModel>.Ok("Transaction updated.", _mapper.Map<TransactionViewModel>(transaction));
        }
    }

}
