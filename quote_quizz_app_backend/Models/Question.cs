using System.ComponentModel.DataAnnotations;

namespace quote_quizz_app_backend.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
