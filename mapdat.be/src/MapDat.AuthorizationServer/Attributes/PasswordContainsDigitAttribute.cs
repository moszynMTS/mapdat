using AuthorizationServer.Resources;

namespace AuthorizationServer.Attributes
{
    public class PasswordContainsDigitAttribute : PasswordCorrectAttribute
    {
        public PasswordContainsDigitAttribute() : base("\\d", ErrorMessages.RessetPassword.MustContainDigit)
        {
        }
    }
}