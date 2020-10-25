using Git.Data;
using Git.Models;
using Git.Services.Contracts;
using Git.ViewModels.UserViewModels;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Git.Services.UserServices
{
    public class UserService : IUsersService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            this._db = db;
        }

        public void CreateUser(CreateUserViewModel userData)
        {
            var currentUser = new User
            {
                Username = userData.Username,
                Email = userData.Email,
                Password = CreatePasswordHash(userData.Password)
            };

            this._db.Users.Add(currentUser);
            this._db.SaveChanges();
        }
        public string GetUserId(UserLoginModel userData)
        {
            var currUser = this._db.Users
                .Where(u => u.Username ==userData.Username && u.Password == CreatePasswordHash(userData.Password))
                .FirstOrDefault();

            return currUser?.Id;
        }

        public bool IsEmailAvailable(string email)
        {
            return !this._db.Users.Any(u => u.Email == email);
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this._db.Users.Any(u => u.Username == username);
        }

        private static string CreatePasswordHash(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);

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