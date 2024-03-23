using AuthorizationServer.Resources;

namespace AuthorizationServer.Attributes
{
    public class PasswordContainsSpecialAttribute : PasswordCorrectAttribute
    {
        private const string pattern = "[!\\@\\#\\$\\%\\^\\&\\*\\(\\)_\\-\\+\\=\\/\\{\\}\\[\\]\\:\\;\\\'\\\"\\\\\\|\\<\\,\\>\\.\\/\\?\\`\\~]";
        //private const string pattern = "[!\\@\\#\\$\\%\\^\\&\\*\\(\\)\\_\\-\\+\\=\\/\\{\\}\\[\\]\\:\\;\\\'\\\"\\\\\\|\\<\\,\\>\\.\\/\\?\\`\\~]";

        public PasswordContainsSpecialAttribute() : base(pattern, ErrorMessages.RessetPassword.MustContaiSpecial)
        {
        }
    }
}