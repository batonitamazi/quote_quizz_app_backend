using quote_quizz_app_backend.Dtos;
using quote_quizz_app_backend.Models;

namespace quote_quizz_app_backend.Services.QuizService
{
    public interface IQuizService
    {
        Task<IEnumerable<Quiz>> GetQuizList();
        Task<IEnumerable<Quiz>> GetQuizzesByTypeAsync(string quizType);
        Task<QuizDto> GetQuizWithQuestionsAndAnswersAsync(int quizId);
        Task SubmitQuizResultAsync(QuizResultDto quizResultDto);

        Task CreateQuiz(QuizDto quiz);

        Task DeleteQuiz(int quizId);



    }
}
