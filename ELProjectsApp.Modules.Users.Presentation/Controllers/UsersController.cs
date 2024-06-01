using ELProjectsApp.Modules.Users.Application.Users;
using ELProjectsApp.Modules.Users.Presentation.Dtos;
using ELProjectsApp.Shared.Kernel.Dtos.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ELProjectsApp.Modules.Users.Presentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    [AllowAnonymous]
    public class UsersController: ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userCreationDto">User information to register in the platform.</param>
        /// <response code="200">A message indicating the user was created.</response>
        /// <response code="500">A message indicating something was wrong with the user creation.</response>
        /// <returns>A message indicating user successfully created.</returns>
        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserCreationRequest userCreationDto)
        {
            var result = await _sender.Send(new AddUser.AddUserCommand(userCreationDto.Email, userCreationDto.Password));
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok("User created successfully");
        }

        /// <summary>
        /// Verify user credentials to create a JWT token.
        /// </summary>
        /// <param name="userAuthentication">User information to log it in.</param>
        /// <returns>Access token and auth information.</returns>
        /// <response code="200">An object with the authentication information.</response>
        /// <response code="400">A message indicating invalid credentials.</response>
        [HttpPost("login")]
        public async Task<ActionResult<JwtTokenDto>> Login(UserAuthenticationDto userAuthentication)
        {
            var result = await _sender
                .Send(new LoginUser.LoginUserCommand(userAuthentication.Email, userAuthentication.Password));
            if(result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }
    }
}
