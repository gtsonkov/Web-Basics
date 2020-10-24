using SULS.ViewModels.Problems;
using SULS.ViewModels.Submissions;
using System.Collections.Generic;

namespace SULS.Services.Contarcts
{
    public interface ISubmissionService
    {
        void AddSubmission(CreateSubmissonViewModel userInput);

        void DeleteSubmission(string submissionId);

        ProblemViewModel GetProblemForSubmitting(string problemId);

        ICollection<SubmisionDetailsViewModel> GetSubbmisionsDetails(string problemId); 
    }
}