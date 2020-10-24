using SULS.ViewModels.Problems;
using System.Collections.Generic;

namespace SULS.Services.Contarcts
{
    public interface IProblemsService
    {
        void CreateProblem (CreateProblemViewModel userInput);

        void AddSubmit();

        ProblemViewModel GetProblemDetails (string problemId);

        bool CheckAvalibleName(string problemName);

        ICollection<ProblemViewModel> GetAllProblems ();
    }
}