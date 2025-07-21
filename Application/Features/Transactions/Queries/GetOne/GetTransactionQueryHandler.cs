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
using Domain.Enums;

namespace Application.Features.Transactions.Queries.GetOne
{
    public class GetTransactionQueryHandler(
        ITransactionRepository transactionRepository,
        IMapper mapper,
        ICurrentUserService currentUserService
        ) : IRequestHandler<GetTransactionQuery, Result<TransactionViewModel>>
    {
        private readonly ITransactionRepository _transactionRepository = transactionRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<TransactionViewModel>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetAsync(x => x.Id == request.Id);

            if (transaction != null && _currentUserService.UserId != transaction.UserId && _currentUserService.Role != Role.SuperAdmin.ToString())
                return Result<TransactionViewModel>.Fail("You are not authorized to see this transaction.");

            if (transaction == null) return Result<TransactionViewModel>.Fail("Transaction not found.");

            return Result<TransactionViewModel>.Ok(_mapper.Map<TransactionViewModel>(transaction));
        }
    }
}
