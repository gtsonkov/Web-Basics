using SULS.Common;
using SULS.Services.Contarcts;
using SULS.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace SULS.Controllers
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
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(UserLoginViewModel input)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var currentUserId = this._userService.GetUserId(input);

            if (currentUserId == null)
            {
               return this.Error(ErrorMessages.InvalidUsernameOrPassword);
            }

            this.SignIn(currentUserId);

            return this.Redirect("/");
        }

        public HttpResponse Logout()
        {
            this.SignOut();
            return Redirect("/");
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
        public HttpResponse Register(CreateUserViewModel input)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            //Check Username
            if (string.IsNullOrEmpty(input.Username) || input.Username.Length < DataRequierments.UsernameMinLength || input.Username.Length > DataRequierments.UsernameMaxLength)
            {
                return this.Error($"Username is required and shoud be between {DataRequierments.UsernameMinLength} and {DataRequierments.UsernameMaxLength} chars.");
            }

            if (!this._userService.IsUsernameAvalible(input.Username))
            {
                return this.Error(ErrorMessages.UsernameNotAvalible);
            }

            //Check Email
            if (string.IsNullOrEmpty(input.Email))
            {
                return this.Error(ErrorMessages.EmailIsRequired);
            }

            if (!new EmailAddressAttribute().IsValid(input.Email))
            {
                return this.Error(ErrorMessages.InvalidEmail);
            }

            //Password Check
            if (string.IsNullOrEmpty(input.Password) || input.Password.Length < DataRequierments.PasswordMinLength || input.Password.Length > DataRequierments.PasswordMaxLength)
            {
                return this.Error($"Password is required and shoud be between {DataRequierments.PasswordMinLength} and {DataRequierments.PasswordMaxLength} chars.");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return this.Error(ErrorMessages.PasswordsDoNotMatch);
            }

            this._userService.CreateUser(input);

            return this.Redirect("/Users/Login");
        }
    }
}