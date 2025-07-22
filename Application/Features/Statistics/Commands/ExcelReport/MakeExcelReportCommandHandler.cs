using Application.Interfaces;
using Application.Models;
using Application.Models.Common;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Statistics.Commands.ExcelReport
{
    public class MakeExcelReportCommandHandler(
        ITransactionRepository transactionRepository,
        IMapper mapper,
        IMessageProducer messageProducer,
        ICurrentUserService currentUserService) : IRequestHandler<MakeExcelReportCommand, Result<bool>>
    {
        private readonly ITransactionRepository _transactionRepository = transactionRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IMessageProducer _messageProducer = messageProducer;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<bool>> Handle(MakeExcelReportCommand request, CancellationToken cancellationToken)
        {
            var exportRequest = await _transactionRepository.GetAllAsync(x => x.UserId == _currentUserService.UserId
                                                                            && DateOnly.FromDateTime(x.CreatedAt) >= request.StartDate
                                                                            && DateOnly.FromDateTime(x.CreatedAt) <= request.EndDate);

            var data = _mapper.Map<List<TransactionViewModel>>(exportRequest);
            await _messageProducer.SendMessage(data);

            return Result<bool>.Ok();
        }
    }
}
