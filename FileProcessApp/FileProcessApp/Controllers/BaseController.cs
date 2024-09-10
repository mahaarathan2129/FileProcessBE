using FileProcessApp.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileProcessingApp.Controllers
{
  
    [Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseController()
        {
            this._httpContextAccessor = HttpContextHelper.GetHttpContextAccessor();
        }
        public long UserId
        {
            get { return GetLoginUserId(); }
        }

        [NonAction]
        public long GetLoginUserId()
        {

            var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
            // Get the username claim from the claims principal - if the user is not authenticated the claim will be null
            string userId =
                claimsPrincipal?.Claims?.SingleOrDefault(c => c.Type == "UserId")?.Value;
            return Convert.ToInt64(userId);
        }
    }
}
