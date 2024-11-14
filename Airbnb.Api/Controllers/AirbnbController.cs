using Airbnb.Api.Common;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Airbnb.Api.Controllers
{
    public class AirbnbController : ControllerBase
    {
        protected long GetUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                Claim? claimId = identity.FindFirst(ClaimTypes.Sid);
                if(claimId != null)
                {
                    string userIdStr = claimId.Value;
                    long userId = 0;
                    if (long.TryParse(userIdStr, out userId))
                    {
                        return userId;
                    }

                }
            }
            throw new UnauthorizedException("Cannot get authorize information");

        }
    }
}
