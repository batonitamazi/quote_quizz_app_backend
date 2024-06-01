using System.ComponentModel.DataAnnotations;



namespace quote_quizz_app_backend.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get;}

        public string PasswordHash { get; set; }


       
    }
}
