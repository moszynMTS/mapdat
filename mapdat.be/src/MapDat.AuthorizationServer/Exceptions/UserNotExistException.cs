using AuthorizationServer.Resources;
using System;

namespace AuthorizationServer.Exceptions
{
    public class UserNotExistException : Exception
    {
        public UserNotExistException(string userName) : base(ErrorMessages.UserNotExists(userName))
        {
        }
    }
}