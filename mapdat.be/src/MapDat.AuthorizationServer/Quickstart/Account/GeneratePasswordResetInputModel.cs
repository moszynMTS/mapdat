using System.ComponentModel.DataAnnotations;

namespace AuthorizationServer.Quickstart.Account
{
    public class GeneratePasswordResetInputModel
    {
        public string ReturnUrl { get; set; }

        [Required]
        public string Username { get; set; }
    }
}