using System.Text.Json.Serialization;

namespace quote_quizz_app_backend.Dtos
{
    public class QuizResultDto
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("quiz_id")]
        public int QuizId { get; set; }

        [JsonPropertyName("correct_answer_count")]
        public int CorrectAnswerCount { get; set; }

        [JsonPropertyName("incorrect_count")]
        public int IncorrectCount { get; set; }

        [JsonPropertyName("total_questions")]
        public int TotalQuestions { get; set; }
    }
}
