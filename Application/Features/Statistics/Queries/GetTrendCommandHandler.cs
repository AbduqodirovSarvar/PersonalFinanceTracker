using Application.Models;
using Application.Models.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Statistics.Queries
{
    public class GetTrendCommandHandler : IRequestHandler<GetTrendCommand, PaginatedResult<CategoryViewModel>>
    {
        public Task<PaginatedResult<CategoryViewModel>> Handle(GetTrendCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
