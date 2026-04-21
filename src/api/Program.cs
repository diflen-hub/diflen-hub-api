using API.Extensions;
using infra.Config;
using application.Config;
using api.Extensions;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJs",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
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
app.UseMiddleware<ApiMiddleware>();

app.UseScalar();
app.UseJwt();

await app.RunAsync();