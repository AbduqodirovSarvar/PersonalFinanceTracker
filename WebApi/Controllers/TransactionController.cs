using Application.Features.Transactions.Commands.Create;
using Application.Features.Transactions.Commands.Delete;
using Application.Features.Transactions.Commands.Update;
using Application.Features.Transactions.Queries.GetList;
using Application.Features.Transactions.Queries.GetOne;
using Application.Models;
using Application.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        /// <param name="command">Transaction creation payload</param>
        /// <returns>Created transaction</returns>
        /// <response code="200">Transaction successfully created</response>
        [HttpPost]
        [ProducesResponseType(typeof(Result<TransactionViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateTransactionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Updates an existing transaction.
        /// </summary>
        /// <param name="command">Transaction update payload</param>
        /// <returns>Updated transaction</returns>
        /// <response code="200">Transaction successfully updated</response>
        [HttpPut]
        [ProducesResponseType(typeof(Result<TransactionViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] UpdateTransactionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Deletes a transaction by ID.
        /// </summary>
        /// <param name="id">Transaction's unique identifier</param>
        /// <returns>Status of delete operation</returns>
        /// <response code="200">Transaction successfully deleted</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(Result<TransactionViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteTransactionCommand(id)));
        }

        /// <summary>
        /// Gets a transaction by ID.
        /// </summary>
        /// <param name="id">Transaction ID</param>
        /// <returns>Transaction data</returns>
        /// <response code="200">Transaction found</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Result<TransactionViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _mediator.Send(new GetTransactionQuery(id)));
        }

        /// <summary>
        /// Gets a paginated list of transactions.
        /// </summary>
        /// <param name="query">Pagination and filtering options</param>
        /// <returns>Paginated transaction list</returns>
        /// <response code="200">Returns list of transactions</response>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<TransactionViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList([FromQuery] GetTransactionListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}