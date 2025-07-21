using Application.Features.Transactions.Commands.Create;
using Application.Features.Transactions.Commands.Delete;
using Application.Features.Transactions.Commands.Update;
using Application.Features.Transactions.Queries.GetList;
using Application.Features.Transactions.Queries.GetOne;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransactionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTransactionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteTransactionCommand(id)));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _mediator.Send(new GetTransactionQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetTransactionListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
