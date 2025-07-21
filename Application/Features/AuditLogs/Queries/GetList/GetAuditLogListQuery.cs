using Application.Models;
using Application.Models.Common;
using Application.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuditLogs.Queries.GetList
{
    public record GetAuditLogListQuery(
        string? SearchTerm = null,
        DateTime? FromDate = null,
        DateTime? ToDate = null
        ) : BasePaginationQuery, IRequest<PaginatedResult<AudiLogViewModel>>;
}
