using BattleCards.Commons;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleCards.Models
{
    public class User
    {
        public User()
        {
            this.UserCards = new HashSet<UserCard>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(ConstantData.UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<UserCard> UserCards { get; set; }
    }
}