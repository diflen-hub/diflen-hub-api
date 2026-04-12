using API;
using API.Extensions;
using infra.Config;
using application.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

Startup.ConfigureJwt(builder.Services, builder.Configuration);
builder.Services.AddScalar();

var app = builder.Build();
app.UseHttpsRedirection();

app.UseScalar();
Startup.UseJwt(app);
Startup.ConfigureMiddlewares(app);

await app.RunAsync();