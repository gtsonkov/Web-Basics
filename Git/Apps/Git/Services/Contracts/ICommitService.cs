using Git.ViewModels.CommitViewModels;
using Git.ViewModels.RepositoryViewModels;
using System.Collections.Generic;

namespace Git.Services.Contracts
{
    public interface ICommitService
    {
        void AddCommit(CreateCommitViewModel userInput);

        void DeleteCommit(string commitId, string userId);

        RepositoryViewModel GetRepositoryInfo(string repoId);

        ICollection<CommitViewModel> GetAllCommits(string repoId, string userId);
    }
}