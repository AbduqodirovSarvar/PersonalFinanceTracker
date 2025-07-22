using Application.Models.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Statistics.Queries
{
    public class GetReportStatusQueryHandler : IRequestHandler<GetReportStatusQuery, Result<bool>>
    {
        public Task<Result<bool>> Handle(GetReportStatusQuery request, CancellationToken cancellationToken)
        {
            // Placeholder for actual implementation
            throw new NotImplementedException();
        }
    }
}
