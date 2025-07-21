using Application.Interfaces;
using Application.Models;
using Application.Models.Common;
using AutoMapper;
using Domain.Entities;
using MediatR;

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
