using Application.Models.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Statistics.Commands.ExcelReport
{
    public record MakeExcelReportCommand(
        DateOnly StartDate,
        DateOnly EndDate
        ) : IRequest<Result<bool>>;
}
