using System.Net;

namespace MapDat.Application.Features.Common
{
    public class BaseResponse
    {
        public BaseResponse(HttpStatusCode statusCode, string? error = null)
        {
            IsSuccess = (int)statusCode < 400;
            StatusCode = statusCode;
            Error = error;
        }

        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? Error { get; set; }
    }

    public class BaseResponse<TContent> : BaseResponse
    {
        public BaseResponse(HttpStatusCode statusCode, TContent? content, string? error = null) : base(statusCode, error)
        {
            IsSuccess = (int)statusCode < 400;
            StatusCode = statusCode;
            Content = content;
            Error = error;
        }

        public TContent? Content { get; set; }
    }
}
