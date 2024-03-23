using AuthorizationServer.Attributes;
using AuthorizationServer.Resources;
using System.ComponentModel.DataAnnotations;

namespace AuthorizationServer.Quickstart.Account
{
    public class ResetPasswordInputModel
    {
        [Required(ErrorMessage = ErrorMessages.RessetPassword.PasswordFieldRequired),
            PasswordContainsDigit(),
            PasswordContainsCapitalCase(),
            PasswordContainsLowerCase(),
            PasswordContainsSpecial(),
            Compare(nameof(PasswordConfirm), ErrorMessage = ErrorMessages.RessetPassword.Missmatch)
            ]
        public string Password { get; set; }

        [Required(ErrorMessage = ErrorMessages.RessetPassword.PasswordConfirmFieldRequired)]
        public string PasswordConfirm { get; set; }

        public bool RequestModelWasInvalid { get; set; }

        public string ReturnUrl { get; set; }

        [Required(ErrorMessage = ErrorMessages.FieldRequired)]
        public string Token { get; set; }

        [Required(ErrorMessage = ErrorMessages.FieldRequired)]
        public string Username { get; set; }
    }
}