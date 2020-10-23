using BattleCards.Commons;
using BattleCards.Services.Contracts;
using BattleCards.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace BattleCards.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel userInput)
        {
            var currUserId = this._userService.GetUserId(userInput.Username,userInput.Password);

            if (currUserId == null)
            {
                return this.Error(ErrorMessages.InvalidUsernameOrPassword);
            }

            return this.Redirect("/Cards/All");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegistrationInputModel userInput)
        {
            if (string.IsNullOrEmpty(userInput.Username) || userInput.Username.Length < ConstantData.UsernameMinLength || userInput.Username.Length > ConstantData.UsernameMaxLength)
            {
                return this.Error($"Username length shoud be between {ConstantData.UsernameMinLength} and {ConstantData.UsernameMaxLength}");
            }

            if (!this._userService.IsUsernameAvalible(userInput.Username))
            {
                return this.Error(ErrorMessages.UsernameNotAvalible);
            }

            if (string.IsNullOrEmpty(userInput.Email) || !new EmailAddressAttribute().IsValid(userInput.Email))
            {
                return this.Error(ErrorMessages.InvalidEmail);
            }

            if (string.IsNullOrEmpty(userInput.Password) || userInput.Password.Length < ConstantData.PasswordMinLength || userInput.Password.Length > ConstantData.PasswordMaxLength)
            {
                return this.Error($"Password length shoud be between {ConstantData.PasswordMinLength} and {ConstantData.PasswordMaxLength} chars.");
            }

            if (userInput.Password != userInput.ConfirmPassword)
            {
                return this.Error(ErrorMessages.PasswordsDoNotMatch);
            }

            this._userService.Create(userInput.Username, userInput.Email, userInput.Password);

            return this.Redirect("/Users/Login");
        }
    }
}