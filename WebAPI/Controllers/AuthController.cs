using Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace WebAPI.Controllers
{

    public class AuthController : BaseController
    {

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            await _mediator.Send(registerCommand);
            return Created();
        }

        //[HttpPost("Login")]
        //public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        //{
        //    var response = await _mediator.Send(loginCommand);
        //    return Ok(response);
        //}
    }
}
