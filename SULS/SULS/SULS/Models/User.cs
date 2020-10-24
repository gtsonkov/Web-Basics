using SULS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SULS.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserSubmissions = new HashSet<Submission>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(DataRequierments.UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<Submission> UserSubmissions { get; set; }
    }
}