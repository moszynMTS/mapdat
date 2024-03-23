using System.Net;

namespace AuthorizationServer.Models
{
    public class CreateAccountOptions
    {
        public string DefaultReturnUrl { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public string GetFormatedMessage(string userName, string token, string returnUrl)
        {
            var url = Url
                .Replace("{UserName}", WebUtility.UrlEncode(userName))
                .Replace("{ReturnUrl}", WebUtility.UrlEncode(returnUrl == null ? DefaultReturnUrl : returnUrl))
                .Replace("{Token}", WebUtility.UrlEncode(token));

            return Message.Replace("{Url}", url).Replace("{Token}", token);
        }
    }
}