using Git.Data;
using Git.Models;
using Git.Services.Contracts;
using Git.ViewModels.RepositoryViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Git.Services.RepositorieServices
{
    public class RepositoryService : IRepositoryService
    {
        private readonly ApplicationDbContext _db;

        public RepositoryService(ApplicationDbContext db)
        {
            this._db = db;
        }

        public void CreateRepository(RepositoryCreateViewModel userInput)
        {
            bool isPublic = userInput.RepositoryType == "Public";

            var currRepo = new Repository
            {
                Name = userInput.Name,
                CreatedOn = DateTime.Now,
                IsPublick = isPublic,
                OwnerId = userInput.OwnerId
            };

            this._db.Repositories.Add(currRepo);
            this._db.SaveChanges();
        }

        public ICollection<RepositoryViewModel> GetAllRepositories()
        {
            var repos = this._db.Repositories
                .Where(x => x.IsPublick)
                .Select(x => new RepositoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOn = x.CreatedOn.ToString("dd.MM.yyyy hh:mm", CultureInfo.InvariantCulture),
                    CommitsCount = x.Commits.Count,
                    Owner = x.Owner.Username
                })
                .ToList();

            return repos;
        }

        public string GetRepositoryId(string repoId) => throw new System.NotImplementedException();
    }
}