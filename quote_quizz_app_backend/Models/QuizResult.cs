namespace quote_quizz_app_backend.Models
{
    public class QuizResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public int CorrectAnswerCount { get; set; }
        public int IncorrectCount { get; set; }
        public int TotalQuestions { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
