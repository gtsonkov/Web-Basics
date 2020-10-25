using Git.Common;
using Git.Services.Contracts;
using Git.ViewModels.UserViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace Git.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            this._userService = userService;
        }

        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Repositories/All");
            }

            return this.View();
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/");
            }

            this.SignOut();
            return Redirect("/");
        }


        [HttpPost]
        public HttpResponse Login(UserLoginModel userInput)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var currentUserId = this._userService.GetUserId(userInput);

            if (string.IsNullOrEmpty(userInput.Username) || string.IsNullOrEmpty(userInput.Password))
            {
                return this.Error(ErrorMessages.EmptyUsernameOrPassword);
            }

            if (currentUserId == null)
            {
                return this.Error(ErrorMessages.InvalidUsernameOrPassword);
            }

            this.SignIn(currentUserId);

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(CreateUserViewModel userInput)
        {
            if (this.IsUserSignedIn())
            {
               return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(userInput.Username) || userInput.Username.Length < DataRequiermentsConst.UsernameMinLength || userInput.Username.Length > DataRequiermentsConst.UsernameMaxLength)
            {
                return this.Error($"Username length shoud be between {DataRequiermentsConst.UsernameMinLength} and {DataRequiermentsConst.UsernameMaxLength}");
            }

            if (!this._userService.IsUsernameAvailable(userInput.Username))
            {
                return this.Error(ErrorMessages.UsernameNotAvalible);
            }

            if (string.IsNullOrEmpty(userInput.Email) || !new EmailAddressAttribute().IsValid(userInput.Email))
            {
                return this.Error(ErrorMessages.InvalidEmail);
            }

            if (string.IsNullOrEmpty(userInput.Password) || userInput.Password.Length < DataRequiermentsConst.PasswordMinLength || userInput.Password.Length > DataRequiermentsConst.PasswordMaxLength)
            {
                return this.Error($"Password length shoud be between {DataRequiermentsConst.PasswordMinLength} and {DataRequiermentsConst.PasswordMaxLength} chars.");
            }

            if (userInput.Password != userInput.ConfirmPassword)
            {
                return this.Error(ErrorMessages.PasswordsDoNotMatch);
            }

            this._userService.CreateUser(userInput);

            return this.Redirect("/Users/Login");
        }
    }
}