using BattleCards.Data;
using BattleCards.Models;
using BattleCards.Services.Contracts;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BattleCards.Services.Users
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
                Email = email,
                Password = ComputeHash(password)
            };

            this._db.Users.Add(currentUser);
            this._db.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var currUser = this._db.Users
                .Where(u => u.Username == username && u.Password == ComputeHash(password))
                .FirstOrDefault();

            return currUser?.Id;  
        }

        public bool IsEmailAvalible(string email) => throw new System.NotImplementedException();

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