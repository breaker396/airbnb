using System.Net;

namespace Airbnb.Api.Common
{
    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException(string message, List<string>? errors = null, HttpStatusCode statusCode = HttpStatusCode.Unauthorized) : base(message, errors, statusCode)
        {
        }
    }
}
