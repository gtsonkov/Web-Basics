using Git.Data;
using Git.Models;
using Git.Services.Contracts;
using Git.ViewModels.CommitViewModels;
using Git.ViewModels.RepositoryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services.CommitServices
{
    public class CommitService : ICommitService
    {
        private readonly ApplicationDbContext _db;

        public CommitService(ApplicationDbContext db)
        {
            this._db = db;
        }

        public void AddCommit(CreateCommitViewModel userInput)
        {
            var currRepo = this._db.Repositories.FirstOrDefault(r => r.Id == userInput.RepoId);

            if (currRepo == null)
            {
                return;
            }

            var currUser = this._db.Users.FirstOrDefault(u => u.Id == userInput.UserId);

            if (currUser == null)
            {
                return;
            }

            var currCommit = new Commit
            {
                Description = userInput.Description,
                CreatedOn = DateTime.Now,
                CreatorId = currUser.Id,
                RepositoryId = currRepo.Id,
            };

            this._db.Commits.Add(currCommit);
            this._db.SaveChanges();
        }

        public void DeleteCommit(string commitId, string userId)
        {
            var currCommit = this._db.Commits.FirstOrDefault(x => x.Id == commitId);
            if (currCommit == null || currCommit.CreatorId != userId)
            {
                return;
            }

            this._db.Commits.Remove(currCommit);
            this._db.SaveChanges();
        }

        public ICollection<CommitViewModel> GetAllCommits(string repoId, string userId)
        {
            var allCommits = this._db
                .Commits
                .Where(u => u.RepositoryId == repoId && u.CreatorId == userId)
                .Select(x => new CommitViewModel
                {
                    Name = x.Repository.Name,
                    CreatedOn = x.CreatedOn.ToString("dd.MM.yyyy hh:mm"),
                    Description = x.Description,
                    Id = x.Id
                })
                .ToList();

            return allCommits;
        }

        public RepositoryViewModel GetRepositoryInfo(string repoId)
        {
            var repoInfo = this._db.Repositories
                .Where(r => r.Id == repoId)
                .Select(x => new RepositoryViewModel
                {
                    Name = x.Name,
                    Id = x.Id
                }
                )
                .FirstOrDefault();

            return repoInfo;
        }
    }
}