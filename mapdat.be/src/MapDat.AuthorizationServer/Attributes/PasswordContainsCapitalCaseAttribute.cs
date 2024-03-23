using AuthorizationServer.Resources;

namespace AuthorizationServer.Attributes
{
    public class PasswordContainsCapitalCaseAttribute : PasswordCorrectAttribute
    {
        public PasswordContainsCapitalCaseAttribute() : base("[A-Z]", ErrorMessages.RessetPassword.MustContainCapitalCase)
        {
        }
    }
}