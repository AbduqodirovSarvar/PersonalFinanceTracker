using Application.Interfaces;
using Application.Models;
using Application.Models.Common;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.Features.AuditLogs.Queries.GetOne
{
    public class GetAuditLogQueryHandler(
        IAuditLogRepository auditLogRepository,
        ICurrentUserService currentUserService,
        IMapper mapper
        ) : IRequestHandler<GetAuditLogQuery, Result<AudiLogViewModel>>
    {
        private readonly IAuditLogRepository _auditLogRepository = auditLogRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<AudiLogViewModel>> Handle(GetAuditLogQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != Role.SuperAdmin.ToString() || _currentUserService.Role != Role.Admin.ToString())
                return Result<AudiLogViewModel>.Fail("You are not authorized to see this log.");

            var auditLog = await _auditLogRepository.GetAsync(a => a.Id == request.Id);
            if (auditLog is null)
                return Result<AudiLogViewModel>.Fail("Audit log not found.");

            return Result<AudiLogViewModel>.Ok(_mapper.Map<AudiLogViewModel>(auditLog));
        }
    }
}
