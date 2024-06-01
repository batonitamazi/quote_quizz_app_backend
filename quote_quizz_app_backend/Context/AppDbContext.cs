using Microsoft.EntityFrameworkCore;
using quote_quizz_app_backend.Models;

namespace quote_quizz_app_backend.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }

        public DbSet<QuizResult> QuizResults { get; set; }
    }
}
