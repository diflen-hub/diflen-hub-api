using API;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

Startup.SetImplementations(builder.Services);
Startup.ConfigureSwagger(builder.Services);
Startup.AddCors(builder.Services);
Startup.IgnoreCycles(builder.Services);
Startup.ConfigureJwt(builder.Services, builder.Configuration);
builder.Services.AddScalar();

var app = builder.Build();

app.UseScalar();
Startup.UseJwt(app);
Startup.ConfigureMiddlewares(app);

Startup.ConfigureAPI(app);
Startup.ConfigureCors(app);

await app.RunAsync();