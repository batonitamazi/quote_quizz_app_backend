using quote_quizz_app_backend.Models;

namespace quote_quizz_app_backend.Services.UserService
{
    public interface IUserService
    {
        Task<User> RegisterUser(string username, string password);
        Task<string> LoginUser(string username, string password);
        Task UpdateUser(int userId, User user);
    }
}
