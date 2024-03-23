namespace AuthorizationServer.Resources
{
    public static class ErrorMessages
    {
        public const string FieldRequired = "Pole wymagane";

        public static string UserNotExists(string userName) => $"Użytkownik {userName} nie istnieje";

        public static class RessetPassword
        {
            public const string Missmatch = "Hasła nie są takie same";
            public const string MustContainCapitalCase = "Hasło musi zawierać dużą literę";
            public const string MustContainDigit = "Hasło musi zawierać cyfrę";
            public const string MustContainLowerCase = "Hasło musi zawierać mała literę";
            public const string MustContaiSpecial = "Hasło musi zawierać znak specjalny";
            public const string PasswordConfirmFieldRequired = "Pole potwierdź hasło jest wymagane";
            public const string PasswordFieldRequired = "Pole hasło jest wymagane";
        }
    }
}