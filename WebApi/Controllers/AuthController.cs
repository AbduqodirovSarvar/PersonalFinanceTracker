using Application.Features.Auth.Commands.ChangePassword;
using Application.Features.Auth.Commands.ChangeRole;
using Application.Features.Auth.Commands.ResetPassword;
using Application.Features.Auth.Commands.SignIn;
using Application.Features.Auth.Commands.SignUp;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("sign-in")]
        [SwaggerOperation(Summary = "User login", Description = "Authenticates the user and returns a token")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("sign-up")]
        [SwaggerOperation(Summary = "User registration", Description = "Registers a new user")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("reset")]
        [SwaggerOperation(Summary = "Reset password", Description = "Resets the user's password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("change-password")]
        [SwaggerOperation(Summary = "Change password", Description = "Changes the current user's password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("change-role")]
        [SwaggerOperation(Summary = "Change user role", Description = "Changes the role of a user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ChangeRole([FromBody] ChangeRoleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
