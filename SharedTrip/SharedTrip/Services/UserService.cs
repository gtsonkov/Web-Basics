using Microsoft.EntityFrameworkCore.Internal;
using SharedTrip.Models;
using SharedTrip.Services.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SharedTrip.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            this._db = db;
        }

        public void Create(string username, string email, string password)
        {
            var currentUser = new User
            {
                Username = username,
                Password = ComputeHash(password),
                Email = email
            };

            this._db.Users.Add(currentUser);
            this._db.SaveChanges();
        }

        public bool IsEmailAvalible(string email)
        {
            return !this._db.Users.Any(x => x.Email == email);
        }

        public string GetUserId(string username, string password)
        {
            var passwordHash = ComputeHash(password);
            var currentUser = this._db.Users.FirstOrDefault(x => x.Username == username && x.Password == passwordHash);

            return currentUser?.Id;
        }

        public bool IsUsernameAvalible(string username)
        {
            return !this._db.Users.Any(x => x.Username == username);
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