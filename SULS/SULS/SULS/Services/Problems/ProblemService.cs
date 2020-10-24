using Microsoft.EntityFrameworkCore.Internal;
using SULS.Data;
using SULS.Models;
using SULS.Services.Contarcts;
using SULS.ViewModels.Problems;
using System.Collections.Generic;
using System.Globalization;
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

        public void DeleteSubmission(string submissionId)
        {
            var currentSubmission = this._db.Submissions.FirstOrDefault(s => s.Id == submissionId);
            this._db.Submissions.Remove(currentSubmission);
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

        public ProblemViewModel GetProblemBySubmissionId(string submissionId)
        {
            var result = this._db
                .Problems
                .Where(p => p.ProblemSubmissions.Any(ps => ps.Id == submissionId))
                .Select(x => new ProblemViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .FirstOrDefault();

            return result;
        }

        public ProblemSubmissionsView GetProblemDetails(string problemId)
        {
            var currentProblemData = this._db.Problems.FirstOrDefault(x => x.Id == problemId);

            var problemInfo = new ProblemSubmissionsView();

            problemInfo.ProblemName = currentProblemData.Name;

            var submissions = this._db.Submissions
                .Where(s => s.ProblemId == problemId)
                .Select(x => new ProblemDetailsViewModel
                {
                    MaxPoints = currentProblemData.Points,
                    AchievedResults = x.AchiveResult,
                    SubmittedOn = x.CreatedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    SubmissionId = x.Id,
                    Username = x.User.Username
                })
                .ToList();

            problemInfo.Submissions = submissions;

            return problemInfo;
        }
    }
}