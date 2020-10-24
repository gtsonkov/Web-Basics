using ConstantData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SULS.Models
{
    public class Problem
    {
        public Problem()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ProblemSubmissions = new HashSet<Submission>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(DataRequierments.ProblemNameMaxLegth)]
        public string Name { get; set; }

        public int Points { get; set; }

        public virtual ICollection<Submission> ProblemSubmissions { get; set; }
    }
}