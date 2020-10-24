using ConstantData;
using System;
using System.ComponentModel.DataAnnotations;

namespace SULS.Models
{
    public class Submission
    {
        public Submission()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(DataRequierments.CodeMaxLength)]
        public string Code { get; set; }

        public int AchiveResult { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public string ProblemId { get; set; }

        public virtual Problem Problem { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}