using Application.Features.Auth.Commands.ChangePassword;
using Application.Features.Auth.Commands.ChangeRole;
using Application.Features.Auth.Commands.ResetPassword;
using Application.Features.Auth.Commands.SignIn;
using Application.Features.Auth.Commands.SignUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("change-role")]
        public async Task<IActionResult> ChangeRole([FromBody] ChangeRoleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
