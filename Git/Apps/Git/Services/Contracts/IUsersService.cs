using Git.ViewModels.UserViewModels;

namespace Git.Services.Contracts
{
    public interface IUsersService
    {
        void CreateUser(CreateUserViewModel userData);

        bool IsEmailAvailable(string email);

        string GetUserId(UserLoginModel userData);

        bool IsUsernameAvailable(string username);
    }
}
