using System.ComponentModel.DataAnnotations;



namespace quote_quizz_app_backend.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public bool IsDisabled { get; set; } = false;



       
    }
}
