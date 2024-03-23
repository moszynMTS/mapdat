using AuthorizationServer.Resources;

namespace AuthorizationServer.Attributes
{
    public class PasswordContainsLowerCaseAttribute : PasswordCorrectAttribute
    {
        public PasswordContainsLowerCaseAttribute() : base("[a-z]", ErrorMessages.RessetPassword.MustContainLowerCase)
        {
        }
    }
}