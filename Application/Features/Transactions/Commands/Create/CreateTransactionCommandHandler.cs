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

namespace Application.Features.Transactions.Commands.Create
{
    public class CreateTransactionCommandHandler(
        ITransactionRepository transactionRepository,
        IMapper mapper)
        : IRequestHandler<CreateTransactionCommand, Result<TransactionViewModel>>
    {
        private readonly ITransactionRepository _transactionRepository = transactionRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<TransactionViewModel>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = new Transaction
            {
                Amount = request.Amount,
                Note = request.Note,
                CategoryId = request.CategoryId
            };

            await _transactionRepository.CreateAsync(entity);

            return Result<TransactionViewModel>.Ok("Transaction created.", _mapper.Map<TransactionViewModel>(entity));
        }
    }

}
