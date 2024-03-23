using System;
using System.Collections.Generic;

namespace AuthorizationServer.Extensions
{
    public static class ExceptionExtensions
    {
        public static IEnumerable<string> ExceptionMessages(this Exception exception)
        {
            yield return exception.Message;

            var innerException = exception.InnerException;
            while (innerException != null)
            {
                yield return innerException.Message;
                innerException = innerException.InnerException;
            }
        }

        public static IEnumerable<string> InnerExceptionMessages(this Exception exception)
        {
            var innerException = exception.InnerException;
            while (innerException != null)
            {
                yield return innerException.Message;
                innerException = innerException.InnerException;
            }
        }

        public static string MessageAndStackTrace(this Exception exception, string message = null)
        {
            return $"{message}\r\nError{exception.ExceptionMessages()}\r\nStackTrace:{exception.StackTrace}";
        }
    }
}