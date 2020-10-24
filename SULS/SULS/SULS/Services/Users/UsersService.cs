using SULS.Data;
using SULS.Models;
using SULS.Services.Contarcts;
using SULS.ViewModels.Users;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SULS.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _db;

        public UsersService(ApplicationDbContext db)
        {
            this._db = db;
        }

        public void CreateUser(CreateUserViewModel userInput)
        {
            var currentUser = new User
            {
                Username = userInput.Username,
                Email = userInput.Email,
                Password = ComputeHash(userInput.Password)
            };

            this._db.Users.Add(currentUser);
            this._db.SaveChanges();
        }

        public string GetUserId(UserLoginViewModel userInput)
        {
            var currentUser = this._db.Users.FirstOrDefault(u => u.Username == userInput.Username && u.Password == ComputeHash(userInput.Password));

            return currentUser?.Id;
        }

        public bool IsUsernameAvalible(string username)
        {
            return !this._db.Users.Any(u => u.Username == username);
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);

            using var hash = SHA512.Create();

            var hashedInputBytes = hash.ComputeHash(bytes);

            var hashedInputStringBuilder = new StringBuilder(128);

            foreach (var b in hashedInputBytes)
            {
                hashedInputStringBuilder.Append(b.ToString("X2"));
            }

            return hashedInputStringBuilder.ToString();
        }
    }
}