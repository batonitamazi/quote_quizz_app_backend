using System.Text.Json.Serialization;

namespace quote_quizz_app_backend.Dtos
{
    public class UserDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName ("username")]
        public string UserName { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("is_disabled")]
        public bool IsDisabled { get; set; }
    }
}
