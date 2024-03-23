namespace AuthorizationServer.Models
{
    public class UpdatePasswordByIdRequestModel
    {
        public string CurrentPassword { get; set; }
        public string Id { get; set; }
        public string NewPassword { get; set; }
    }
}