using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Queries.GetList;
using Application.Features.Users.Queries.GetOne;
using Application.Models.Common;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="command">User update payload</param>
        /// <returns>Updated user data</returns>
        /// <response code="200">User successfully updated</response>
        [HttpPut]
        [ProducesResponseType(typeof(Result<UserViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">User's unique identifier</param>
        /// <returns>Status of delete operation</returns>
        /// <response code="200">User successfully deleted</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(Result<UserViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteUserCommand(id)));
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User data</returns>
        /// <response code="200">User found</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Result<UserViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetUserQuery(UserId: id);
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Gets a user by filter (username or email).
        /// </summary>
        /// <param name="query">Filter query (username or email required)</param>
        /// <returns>Matched user</returns>
        /// <response code="200">User found</response>
        [HttpGet("by-filter")]
        [ProducesResponseType(typeof(Result<UserViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByFilter([FromQuery] GetUserQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Gets a paginated list of users.
        /// </summary>
        /// <param name="query">Pagination, sorting, and filtering options</param>
        /// <returns>Paginated user list</returns>
        /// <response code="200">Returns list of users</response>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<UserViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList([FromQuery] GetUserListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
