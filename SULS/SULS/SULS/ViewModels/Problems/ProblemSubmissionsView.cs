using System.Collections.Generic;

namespace SULS.ViewModels.Problems
{
    public class ProblemSubmissionsView
    {
        public ProblemSubmissionsView()
        {
            this.Submissions = new List<ProblemDetailsViewModel>();
        }

        public string ProblemName { get; set; }

        public ICollection<ProblemDetailsViewModel> Submissions { get; set; }
    }
}