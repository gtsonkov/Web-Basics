using Git.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git.Models
{
    public class Repository
    {
        public Repository()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Commits = new HashSet<Commit>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(DataRequiermentsConst.RepositoryNameMaxLength)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsPublick { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<Commit> Commits { get; set; }
    }
}