using Scalar.AspNetCore;

namespace API.Extensions
{
    public static class ScalarExtension
    {
        public static void AddScalar(this IServiceCollection services)
        {
            services.AddOpenApi();
        }

        public static void UseScalar(this WebApplication app)
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
    }
}