using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace quote_quizz_app_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class QuotesController : ControllerBase
    {
        [HttpGet("quizzes")]
        public async Task<IActionResult> GetQuizzes()
        {
            return Ok();
        }
    }
}
