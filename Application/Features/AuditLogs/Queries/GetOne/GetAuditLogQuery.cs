using Application.Models;
using Application.Models.Common;
using MediatR;

namespace Application.Features.AuditLogs.Queries.GetOne
{
    public record GetAuditLogQuery(Guid Id) : IRequest<Result<AudiLogViewModel>>
    {
    }
}
