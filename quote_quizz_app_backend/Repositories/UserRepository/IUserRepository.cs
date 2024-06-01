using quote_quizz_app_backend.Models;

namespace quote_quizz_app_backend.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int userId);
        Task<User> GetUserByUsername(string username);
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int userId);
    }
}
