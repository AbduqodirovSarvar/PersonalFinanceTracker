using Application.Features.Categories.Commands.Create;
using Application.Features.Categories.Commands.Delete;
using Application.Features.Categories.Commands.Update;
using Application.Features.Categories.Queries.GetList;
using Application.Features.Categories.Queries.GetOne;
using Application.Models;
using Application.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// API for managing categories.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="command">The category data.</param>
        /// <returns>The created category information.</returns>
        /// <response code="200">Category created successfully.</response>
        /// <response code="400">Invalid input data.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="command">The updated category data.</param>
        /// <returns>The updated category information.</returns>
        /// <response code="200">Category updated successfully.</response>
        /// <response code="404">Category not found.</response>
        [HttpPut]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>Deletion result.</returns>
        /// <response code="200">Category deleted successfully.</response>
        /// <response code="404">Category not found.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteCategoryCommand(id)));
        }

        /// <summary>
        /// Gets a category by ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>The category information.</returns>
        /// <response code="200">Category found.</response>
        /// <response code="404">Category not found.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Result<CategoryViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _mediator.Send(new GetCategoryQuery(id)));
        }

        /// <summary>
        /// Gets a list of categories with optional filtering.
        /// </summary>
        /// <param name="query">Query parameters for filtering, paging, etc.</param>
        /// <returns>A list of categories.</returns>
        /// <response code="200">List retrieved successfully.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<CategoryViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList([FromQuery] GetCategoryListQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
