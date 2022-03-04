using kamafi.auth.core.middlewares;

namespace kamafi.auth.core.extensions
{
    public static class AuthExceptionExtensions
    {
        public static IApplicationBuilder UseAuthExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthExceptionMiddleware>();
        }
    }
}
