using Microsoft.EntityFrameworkCore.Internal;
using SULS.Data;
using SULS.Models;
using SULS.Services.Contarcts;
using SULS.ViewModels.Problems;
using System.Collections.Generic;
using System.Linq;

namespace SULS.Services.Problems
{
    public class ProblemService : IProblemsService
    {
        private readonly ApplicationDbContext _db;

        public ProblemService(ApplicationDbContext db)
        {
            this._db = db;
        }

        public void AddSubmit() => throw new System.NotImplementedException();

        public bool CheckAvalibleName(string problemName)
        {
            return !this._db.Problems.Any(p => p.Name == problemName);
        }

        public void CreateProblem(CreateProblemViewModel userInput)
        {
            var currentProblem = new Problem
            {
                Name = userInput.Name,
                Points = userInput.Points
            };

            this._db.Problems.Add(currentProblem);
            this._db.SaveChanges();
        }

        public ICollection<ProblemViewModel> GetAllProblems()
        {
            var problems = this._db.Problems
                .Select(p => new ProblemViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Submissions = p.ProblemSubmissions.Count
                }
                ).ToList();

            return problems;
        }

        public ProblemViewModel GetProblemDetails(string problemId) => throw new System.NotImplementedException();
    }
}