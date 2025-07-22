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
    public record GetTrendCommand() : IRequest<PaginatedResult<CategoryViewModel>>;
}
