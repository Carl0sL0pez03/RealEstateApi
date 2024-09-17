using Application.Dtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

using Domain.Entities;

namespace Api.Controllers
{
    /// <summary>
    /// Controller responsible for user authentication and registration.
    /// Provides endpoints for login and user registration.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        private readonly AuthService _authService = authService;

        /// <summary>
        /// Authenticates a user and generates a JWT token if the credentials are valid.
        /// </summary>
        /// <param name="loginDto">The login data transfer object containing the username and password.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a JSON Web Token (JWT) if authentication is successful.
        /// </returns>
        /// <response code="200">Returns the JWT token upon successful login.</response>
        /// <response code="400">If the login data is invalid or authentication fails.</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _authService.AuthenticateAsync(loginDto.Username, loginDto.Password);
            return Ok(new { Token = token });
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="registerDto">The registration data transfer object containing the username and password.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating whether the registration was successful.
        /// </returns>
        /// <response code="200">Returns a success message if the registration was successful.</response>
        /// <response code="400">If the registration data is invalid.</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = registerDto.Password
            };

            await _authService.RegisterAsync(user);
            return Ok("User registered successfully");
        }

    }
}