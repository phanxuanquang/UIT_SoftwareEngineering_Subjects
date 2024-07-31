using Microsoft.AspNetCore.Http;

namespace Helper
{
    public static class HttpContextHelper
    {
        private static IHttpContextAccessor _accessor;

        public static void Configure(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public static string? GetAccessKey()
        {
            if (!_accessor.HttpContext.Request.Headers.TryGetValue("Authentication", out var accessKey))
            {
                return null;
            }

            var key = accessKey.ToString().Trim();
            if (!key.StartsWith("AIza") && !key.StartsWith("ya29."))
            {
                return null;
            }

            return key;
        }
    }
}
