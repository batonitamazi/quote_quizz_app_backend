using Microsoft.Extensions.Configuration;
using quote_quizz_app_backend.Models;
using quote_quizz_app_backend.Repositories.UserRepository;
using System.Text;
using System.Security.Cryptography;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;


namespace quote_quizz_app_backend.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public async Task<User> RegisterUser(string username, string password)
        {
            var existingUser = await _userRepository.GetUserByUsername(username);
            if (existingUser != null)
            {
                throw new Exception("User with the given username already exists.");
            }

            var hashedPassword = HashPassword(password);
            var newUser = new User { UserName = username, PasswordHash = hashedPassword };
            await _userRepository.CreateUser(newUser);

            return newUser;
        }
        public async Task<string> LoginUser(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                throw new Exception("Invalid username or password.");
            }

            var token = GenerateJwtToken(user);
            return token;
        }


        public async Task UpdateUser(int userId, User user)
        {
            var existingUser = await _userRepository.GetUserById(userId);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            existingUser.UserName = user.UserName;
            existingUser.PasswordHash = user.PasswordHash;

            await _userRepository.UpdateUser(existingUser);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (var byteValue in bytes)
                {
                    builder.Append(byteValue.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }

    }
}
