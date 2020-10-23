namespace BattleCards.Services.Contracts
{
    public interface IUserService
    {
        string GetUserId(string username, string password);

        void Create(string username, string email, string password);

        bool IsUsernameAvalible(string username);

        bool IsEmailAvalible(string email);
    }
}