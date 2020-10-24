using SULS.Data;
using SULS.Models;
using SULS.ViewModels.Problems;
using SULS.ViewModels.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SULS.Services.Contarcts.Submissions
{
    public class SubmissionService : ISubmissionService
    {
        private readonly ApplicationDbContext _db;

        private readonly Random RandomGenerator;

        public SubmissionService(ApplicationDbContext db)
        {
            this._db = db;
            this.RandomGenerator = new Random();
        }

        public void AddSubmission(CreateSubmissonViewModel userInput)
        {
            var currentSubmission = new Submission
            {
                UserId = userInput.UserId,
                ProblemId = userInput.ProblemId,
                CreatedOn = DateTime.Now,
                Code = userInput.Code,
                AchiveResult = CalculateAchievmentResult(userInput.ProblemId),
            };

            this._db.Submissions.Add(currentSubmission);
            this._db.SaveChanges();
        }

        public void DeleteSubmission(string submissionId) => throw new System.NotImplementedException();
        public ProblemViewModel GetProblemForSubmitting(string problemId)
        {
            var currentProblem = this._db.Problems
                .Where(p => p.Id == problemId)
                .Select(p => new ProblemViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Submissions = p.ProblemSubmissions.Count

                })
                .FirstOrDefault();

            return currentProblem;
                
        }

        public ICollection<SubmisionDetailsViewModel> GetSubbmisionsDetails(string problemId) => throw new System.NotImplementedException();

        private int CalculateAchievmentResult(string problemId)
        {
            var currProblemPoints = this._db.Problems.FirstOrDefault(x => x.Id == problemId);
            int randomMax = currProblemPoints.Points;

            int randomNumber = this.RandomGenerator.Next(0, randomMax + 1);

            int result = Convert.ToInt32(Math.Floor(((Convert.ToDecimal(randomNumber) / randomMax) * 100)));

            return result;
        }
    }
}