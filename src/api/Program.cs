using API.Extensions;
using infra.Config;
using application.Config;
using api.Extensions;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.AddOpenTelemetryExtension();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJs",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddJwt(builder.Configuration);
builder.Services.AddScalar();

var app = builder.Build();

app.UseCors("AllowNextJs");

app.UseHttpsRedirection();

// Handle CORS preflight requests before authentication
app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 200;
        await context.Response.CompleteAsync();
        return;
    }
    await next();
});

app.UseMiddleware<ApiMiddleware>();

app.UseScalar();
app.UseJwt();

await app.RunAsync();
