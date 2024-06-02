using quote_quizz_app_backend.Models;

namespace quote_quizz_app_backend.Repositories.QuizRepository
{
    public interface IQuizRepository
    {
        Task<IEnumerable<Quiz>> GetQuizzes();
        Task<IEnumerable<Quiz>> GetQuizzesByTypeAsync(string quizType);
        Task<Quiz> GetQuizWithQuestionsAndAnswersAsync(int quizId);
        Task SaveQuizResultAsync(QuizResult quizResult);

        Task CreateQuiz(Quiz quiz); 
        Task DeleteQuiz(Quiz quiz);



    }
}
