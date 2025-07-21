using Application.Models;
using Application.Models.Common;
using Application.Models.Queries;
using MediatR;

namespace Application.Features.AuditLogs.Queries.GetList
{
    public record GetAuditLogListQuery(
        string? SearchTerm = null,
        DateTime? FromDate = null,
        DateTime? ToDate = null
        ) : BasePaginationQuery, IRequest<PaginatedResult<AudiLogViewModel>>;
}
