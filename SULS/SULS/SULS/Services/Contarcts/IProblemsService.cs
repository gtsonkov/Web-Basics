using SULS.ViewModels.Problems;
using System.Collections.Generic;

namespace SULS.Services.Contarcts
{
    public interface IProblemsService
    {
        void CreateProblem (CreateProblemViewModel userInput);

        ProblemSubmissionsView GetProblemDetails (string problemId);

        bool CheckAvalibleName(string problemName);

        ICollection<ProblemViewModel> GetAllProblems ();
    }
}