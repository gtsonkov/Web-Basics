using Git.ViewModels.RepositoryViewModels;
using System.Collections.Generic;

namespace Git.Services.Contracts
{
    public interface IRepositoryService
    {
        void CreateRepository(RepositoryCreateViewModel userInput);

        string GetRepositoryId(string repoId);

        ICollection<RepositoryViewModel> GetAllRepositories();
    }
}