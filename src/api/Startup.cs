using System.Text;
using API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API
{
    internal static class Startup
    {
        internal static void ConfigureJwt(IServiceCollection services, ConfigurationManager configuration)
        {
            var key = configuration["JwtConfig:Key"] ?? throw new KeyNotFoundException("The environment was not prepared to set key");

            services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = configuration["JwtConfig:Issuer"],
                    ValidAudience = configuration["JwtConfig:Audience"],
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

            services.AddAuthorization();
        }

        internal static void ConfigureMiddlewares(WebApplication app)
        {
            app.UseMiddleware<ApiMiddleware>();
        }

        internal static void UseJwt(WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}