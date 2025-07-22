using Application.Features.AuditLogs.Queries.GetList;
using Application.Features.AuditLogs.Queries.GetOne;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuditLogController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Get an audit log by ID.
        /// </summary>
        /// <param name="id">Audit log ID (GUID)</param>
        /// <returns>Returns a single audit log entry</returns>
        /// <response code="200">Audit log found and returned successfully</response>
        /// <response code="404">Audit log not found</response>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _mediator.Send(new GetAuditLogQuery(id)));
        }

        /// <summary>
        /// Get a list of audit logs.
        /// </summary>
        /// <param name="query">Query parameters for filtering, pagination, etc.</param>
        /// <returns>Returns a list of audit logs</returns>
        /// <response code="200">Audit logs fetched successfully</response>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetAuditLogListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
