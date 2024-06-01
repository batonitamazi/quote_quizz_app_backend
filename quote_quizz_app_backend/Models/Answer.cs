using System.ComponentModel.DataAnnotations;

namespace quote_quizz_app_backend.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
