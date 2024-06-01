using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quote_quizz_app_backend.Context;
using quote_quizz_app_backend.Dtos;
using quote_quizz_app_backend.Models;
using quote_quizz_app_backend.Services.UserService;

namespace quote_quizz_app_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;


        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            try
            {
                await _userService.RegisterUser(model.Username, model.Password);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            try
            {
                var token = await _userService.LoginUser(model.Username, model.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }


    }
}
