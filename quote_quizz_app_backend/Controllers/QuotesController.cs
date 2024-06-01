using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using quote_quizz_app_backend.Dtos;
using quote_quizz_app_backend.Services.QuizService;
using System.Security.Claims;

namespace quote_quizz_app_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class QuotesController : ControllerBase
    {
        public readonly IQuizService quizService;

        public QuotesController(IQuizService quizService)
        {
            this.quizService = quizService;
        }

        [HttpGet("quizzes")]
        public async Task<IActionResult> GetQuizzes()
        {
            var quizzes = await quizService.GetQuizList();
            return Ok(quizzes);
        }

        [HttpGet("byType/{quizType}")]
        public async Task<IActionResult> GetQuizzesByType(string quizType)
        {
            var quizzes = await quizService.GetQuizzesByTypeAsync(quizType);
            return Ok(quizzes);
        }

        [HttpGet("{quizId}/withQuestionsAndAnswers")]
        public async Task<IActionResult> GetQuizWithQuestionsAndAnswers(int quizId)
        {
            var quiz = await quizService.GetQuizWithQuestionsAndAnswersAsync(quizId);
            if (quiz == null)
            {
                return NotFound();
            }
            return Ok(quiz);
        }
        [HttpPost("saveUserQuizResult")]
        public async Task<IActionResult> SaveUserQuizResult(QuizResultDto data)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            data.UserId = userId;
            await quizService.SubmitQuizResultAsync(data);
            return Ok(new { message = "Quiz result submitted successfully" });
        }

    }
}
