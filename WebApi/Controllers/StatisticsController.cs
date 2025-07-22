using Application.Features.Files.Queries;
using Application.Features.Statistics.Commands.ExcelReport;
using Application.Features.Statistics.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        /// <summary>
        /// Returns the user's category spending statistics.
        /// </summary>
        /// <returns>List of category spendings</returns>
        [HttpGet("category-spending")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategorySpending([FromQuery] GetCategorySpendingCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Returns the user's spending trend over time.
        /// </summary>
        /// <returns>Trend data over a period</returns>
        [HttpGet("trend")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTrend()
        {
            return Ok(await _mediator.Send(new GetTrendCommand()));
        }

        /// <summary>
        /// Exports the user's category spending data to Excel.
        /// </summary>
        /// <returns>Excel export file or task started response</returns>
        [HttpGet("export/category-spending")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportCategorySpending([FromQuery] MakeExcelReportCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Returns the export status of a specific export operation.
        /// </summary>
        /// <param name="id">Export operation ID</param>
        /// <returns>Status of the export task</returns>
        [HttpGet("status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReportStatus()
        {
            return Ok(await _mediator.Send(new GetReportStatusQuery()));
        }

        /// <summary>
        /// Downloads a specific exported file by filename.
        /// </summary>
        /// <param name="fileName">Name of the exported file</param>
        /// <returns>File download</returns>
        [HttpGet("export/download/{fileName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DownloadExportFile(string fileName)
        {
            var bytes = await _mediator.Send(new GetFileQuery(fileName));
            if (bytes == null)
                return NotFound();

            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}
