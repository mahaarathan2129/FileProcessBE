using FileProcessingApp.Models.Entities;

namespace FileProcessApp.Common
{
    public static class HttpContextHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void SetHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static IHttpContextAccessor GetHttpContextAccessor()
        {
            return _httpContextAccessor;
        }

        public static HttpContext GetCurrentHttpContext()
        {
            return _httpContextAccessor?.HttpContext;
        }

        public static string GetClaimsValueByKey(string key)
        {
            return _httpContextAccessor?.HttpContext.User.Claims?.SingleOrDefault(c => c.Type == key)?.Value;
           ;
        }
    }

}
