using System.ComponentModel.DataAnnotations;

namespace quote_quizz_app_backend.Models
{
    public class Quiz
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
