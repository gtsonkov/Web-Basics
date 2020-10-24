using SULS.ViewModels.Users;

namespace SULS.Services.Contarcts
{
    public interface IUsersService
    {
        void CreateUser(CreateUserViewModel userInput);

        string GetUserId(UserLoginViewModel userInput);

        bool IsUsernameAvalible(string username);
    }
}