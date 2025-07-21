using Application.Interfaces;
using Application.Models.Common;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Features.AuditLogs.Queries.GetList
{
    public class GetAuditLogListQueryHandler(
        IAuditLogRepository auditLogRepository,
        ICurrentUserService currentUserService,
        IMapper mapper
    ) : IRequestHandler<GetAuditLogListQuery, PaginatedResult<AudiLogViewModel>>
    {
        private readonly IAuditLogRepository _auditLogRepository = auditLogRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IMapper _mapper = mapper;

        public async Task<PaginatedResult<AudiLogViewModel>> Handle(GetAuditLogListQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != Role.SuperAdmin.ToString() || _currentUserService.Role != Role.Admin.ToString())
                return PaginatedResult<AudiLogViewModel>.Fail("You are not authorized to view audit logs.");

            Expression<Func<AuditLog, bool>> predicate = log =>
                (string.IsNullOrWhiteSpace(request.SearchTerm) ||
                log.EntityName != null && log.EntityName.ToLower().Contains(request.SearchTerm.ToLower()) ||
                log.Action != null && log.Action.ToLower().Contains(request.SearchTerm.ToLower()) ||
                log.OldValue != null && log.OldValue.ToLower().Contains(request.SearchTerm.ToLower()) ||
                log.NewValue != null && log.NewValue.ToLower().Contains(request.SearchTerm.ToLower())) &&
                (!request.FromDate.HasValue || log.CreatedAt >= request.FromDate.Value) &&
                (!request.ToDate.HasValue || log.CreatedAt <= request.ToDate.Value);

            Func<IQueryable<AuditLog>, IOrderedQueryable<AuditLog>> orderBy = request.SortBy?.ToLower() switch
            {
                "entityname" => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(l => l.EntityName)
                    : q => q.OrderByDescending(l => l.EntityName),

                "action" => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(l => l.Action)
                    : q => q.OrderByDescending(l => l.Action),

                "createdat" => request.SortDirection.ToLower() == "asc"
                    ? q => q.OrderBy(l => l.CreatedAt)
                    : q => q.OrderByDescending(l => l.CreatedAt),

                _ => q => q.OrderByDescending(l => l.CreatedAt)
            };

            var (logs, totalItems) = await _auditLogRepository.GetPaginatedAsync(
                predicate,
                request.PageIndex,
                request.PageSize,
                orderBy
            );

            var viewModels = _mapper.Map<List<AudiLogViewModel>>(logs);

            return PaginatedResult<AudiLogViewModel>.Ok(viewModels, request.PageIndex, request.PageSize, totalItems);
        }
    }
}
