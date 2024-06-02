using Microsoft.EntityFrameworkCore;
using quote_quizz_app_backend.Context;
using quote_quizz_app_backend.Models;

namespace quote_quizz_app_backend.Repositories.QuizRepository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _context;

        public QuizRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Quiz>> GetQuizzes()
        {
            return await _context.Quizzes.ToListAsync();
        }
        public async Task<IEnumerable<Quiz>> GetQuizzesByTypeAsync(string quizType)
        {
            return await _context.Quizzes.Where(q => q.Type == quizType).ToListAsync();
        }
        public async Task<Quiz> GetQuizWithQuestionsAndAnswersAsync(int quizId)
        {
            return await _context.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == quizId);
        }

        public async Task SaveQuizResultAsync(QuizResult quizResult)
        {
            await _context.QuizResults.AddAsync(quizResult);
            await _context.SaveChangesAsync();
        }

        public async Task CreateQuiz(Quiz quizz)
        {
            await _context.Quizzes.AddAsync(quizz);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuiz(Quiz quiz)
        {
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
        }

    }
}
