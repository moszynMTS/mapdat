namespace AuthorizationServer.Models
{
    public class ChangePasswordRequestModel
    {
        public string UserName { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
