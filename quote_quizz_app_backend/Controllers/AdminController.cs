using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quote_quizz_app_backend.Dtos;
using quote_quizz_app_backend.Models;
using quote_quizz_app_backend.Services.QuizService;
using quote_quizz_app_backend.Services.UserService;

namespace quote_quizz_app_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        public readonly IUserService userService;

        public readonly IQuizService quizService;
        public AdminController(IUserService userService, IQuizService quizService)
        {
            this.userService = userService;
            this.quizService = quizService;
        }


        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto user)
        {

            try
            {
                await userService.RegisterUser(user.UserName, user.Password);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto user)
        {
            try
            {
                await userService.UpdateUser(id, user);
                return Ok("User updated  successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("deleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await userService.DeleteUser(id);
                return Ok("User deleted  successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("users")]
        public async Task<IActionResult> ListUsers()
        {
            var users = await userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("quizzes")]
        public async Task<IActionResult> GetQuizzes()
        {
            var quotes = await quizService.GetQuizList();
            return Ok(quotes);
        }

        [HttpGet("getQuizzById")]
        public async Task<ActionResult<QuizDto>> GetQuizById(int id)
        {
            var quizz = await quizService.GetQuizWithQuestionsAndAnswersAsync(id);
            if (quizz == null)
            {
                return NotFound();
            }
            return quizz;
        }

        [HttpPost("createQuizz")]
        public async Task<ActionResult<QuizDto>> CreateQuiz([FromBody] QuizDto dto)
        {
            await quizService.CreateQuiz(dto);
            return Ok();
        }

        [HttpDelete("deleteQuizz")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            await quizService.DeleteQuiz(id);
            return NoContent();
        }



    }
}
