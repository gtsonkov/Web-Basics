using SharedTrip.Common.Constants;
using SharedTrip.Services.Contracts;
using SharedTrip.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Controllers
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
        public HttpResponse Login(LoginInputModel userData)
        {
            var userId = this._userService.GetUserId(userData.Username, userData.Password);

            if (userId == null)
            {
                return this.Error(ErrorMessages.InvalidUsernameOrPassword);
            }

            this.SignIn(userId);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel userData)
        {
            //Username requierments check
            if (string.IsNullOrEmpty(userData.Username) || userData.Username.Length < UserDataRequirements.UsernameMinLength || userData.Username.Length > UserDataRequirements.UsernameMaxLength)
            {
                return this.Error($"Username length shoud be between {UserDataRequirements.UsernameMinLength} and {UserDataRequirements.UsernameMaxLength} chars.");
            }

            if (!this._userService.IsUsernameAvalible(userData.Username))
            {
                return this.Error(ErrorMessages.UsernameNotAvalible);
            }

            //Email requierments check
            if (string.IsNullOrEmpty(userData.Email) || !new EmailAddressAttribute().IsValid(userData.Email))
            {
                return this.Error(ErrorMessages.InvalidEmail);
            }

            if (!this._userService.IsEmailAvalible(userData.Email))
            {
                return this.Error(ErrorMessages.EmailNotAvalible);
            }

            //Password requierments check
            if (string.IsNullOrEmpty(userData.Password) || userData.Password.Length < UserDataRequirements.PasswordMinLength || userData.Password.Length > UserDataRequirements.PasswordMaxLength)
            {
                return this.Error($"Password length shoud be between {UserDataRequirements.PasswordMinLength} and {UserDataRequirements.PasswordMaxLength} chars.");
            }

            if (userData.Password != userData.ConfirmPassword)
            {
                return this.Error(ErrorMessages.PasswordsDoNotMatch);
            }

            this._userService.Create(userData.Username,userData.Email,userData.Password);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            this.SignOut();
            return Redirect("/");
        }
    }
}