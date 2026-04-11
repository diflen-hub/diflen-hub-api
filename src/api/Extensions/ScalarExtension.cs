using Scalar.AspNetCore;

namespace API.Extensions
{
    public static class ScalarExtension
    {
        public static void AddScalar(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddOpenApi();
        }

        public static void UseScalar(this WebApplication app)
        {
            app.MapControllers();
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
    }
}