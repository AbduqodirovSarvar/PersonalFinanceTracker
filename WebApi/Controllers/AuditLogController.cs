using Application.Features.AuditLogs.Queries.GetList;
using Application.Features.AuditLogs.Queries.GetOne;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _mediator.Send(new GetAuditLogQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetAuditLogListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
