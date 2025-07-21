using Application.Models;
using Application.Models.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuditLogs.Queries.GetOne
{
    public record GetAuditLogQuery(Guid Id) : IRequest<Result<AudiLogViewModel>>
    {
    }
}
