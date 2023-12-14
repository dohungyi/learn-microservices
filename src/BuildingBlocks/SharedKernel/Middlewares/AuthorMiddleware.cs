using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace SharedKernel.Middlewares
{
    public class AuthorMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("microservices-author", "Do Chi Hung");
            context.Response.Headers.Add("microservices-facebook", "https://facebook.com/");
            context.Response.Headers.Add("microservices-email", "dchcgl2002@gmail.com");
            context.Response.Headers.Add("microservices-contact", "0976580418");

            await _next(context);
        }

    }

    public static class AuthorMiddlewareMiddlewareExtension
    {
        public static IApplicationBuilder UseCoreAuthor(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthorMiddleware>();
        }
    }
}
