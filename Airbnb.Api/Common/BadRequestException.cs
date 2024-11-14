using System.Net;

namespace Airbnb.Api.Common
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message, List<string>? errors = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, errors, statusCode)
        {
        }
    }
}
