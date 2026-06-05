using System.Threading.Tasks;
using AICMS.Core.Routing;
using Microsoft.AspNetCore.Http;

namespace AICMS.Web.Middleware
{
    /// <summary>
    /// Middleware that detects language and attaches RouteContext to HttpContext.Items
    /// </summary>
    public class MultiLanguageMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly LanguageRouter _router;

        public MultiLanguageMiddleware(RequestDelegate next, LanguageRouter router)
        {
            _next = next;
            _router = router;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value ?? "/";
            var query = context.Request.Query.ToDictionary(k => k.Key, v => v.Value.ToString());
            var routeContext = _router.ParseRoute(path, query);
            context.Items["RouteContext"] = routeContext;
            await _next(context);
        }
    }

    // Extension to register middleware easily
    public static class MultiLanguageMiddlewareExtensions
    {
        public static IApplicationBuilder UseMultiLanguage(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MultiLanguageMiddleware>();
        }
    }
}
