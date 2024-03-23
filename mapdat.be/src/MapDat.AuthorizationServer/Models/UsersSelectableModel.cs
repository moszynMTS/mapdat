namespace MapDat.AuthorizationServer.Models
{
    public class UsersSelectableModel : SelectlistResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }   
    }
}
