using BLL.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApplicationServer.ViewModels;

namespace TestApplicationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> logger;
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
        {
            this.logger = logger;
            this.authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("Model state is not valid");
                return StatusCode(StatusCodes.Status406NotAcceptable, "Model state is not valid");
            }

            var registerUser = convert(registerViewModel);

            try
            {
                await authenticationService.RegisterAsync(registerUser);
                return Ok("User created successfully");
            }
            catch (ArgumentException ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, "Model state is not valid");
            }

            var loginUser = convert(loginViewModel);

            try
            {
                var token = await authenticationService.LoginAsync(loginUser);
                return Ok(token);
            }
            catch (ArgumentException ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
            }
        }

        private RegisterUserModel convert(RegisterViewModel registerViewModel)
        {
            return new RegisterUserModel
            {
                Login = registerViewModel.Login,
                Email = registerViewModel.Email,
                Password = registerViewModel.Password,
                ConfirmPassword = registerViewModel.ConfirmPassword,
                Role = registerViewModel.Role
            };
        }

        private LoginUserModel convert(LoginViewModel loginViewModel)
        {
            return new LoginUserModel
            {
                Login = loginViewModel.Login,
                Password = loginViewModel.Password
            };
        }
    }
}
