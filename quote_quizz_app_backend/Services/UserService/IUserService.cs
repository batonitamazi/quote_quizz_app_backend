using quote_quizz_app_backend.Dtos;
using quote_quizz_app_backend.Models;

namespace quote_quizz_app_backend.Services.UserService
{
    public interface IUserService
    {
        Task<User> RegisterUser(string username, string password);
        Task<string> LoginUser(string username, string password);
        Task UpdateUser(int userId, UserDto user);
        Task DeleteUser (int userId);
        Task<List<UserDto>> GetUsers();
    }
}
