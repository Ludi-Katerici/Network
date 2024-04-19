using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.Cookies;
using Server.API;
using Server.Common.Infrastructure;
using Server.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePOCO<DatabaseOptions>(builder.Configuration);

builder.Services
    .AddLogging()
    .AddCommonModule()
    .AddInfrastructureLayer()
    .AddApiModule()
    .AddCors(x => x.AddDefaultPolicy(y => y.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()))
    .AddDateOnlyTimeOnlyStringConverters()
    .AddOutputCache()
    .AddFastEndpoints(c => c.Assemblies = new[]
    {
        typeof(Program).Assembly,
    })
    .SwaggerDocument()
    .AddAuthorization()
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
        CookieAuthenticationDefaults.AuthenticationScheme,
        o => {
            o.ExpireTimeSpan = TimeSpan.FromDays(1);
            o.Cookie.MaxAge = TimeSpan.FromDays(1);
            o.Cookie.HttpOnly = false;
            o.Cookie.SameSite = SameSiteMode.Lax;
        });
        
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policyBuilder => policyBuilder.WithOrigins("https://localhost:7040")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowSpecificOrigin");
}

app.UseAuthentication()
    .UseAuthorization()
    .UseOutputCache();

app.UseFastEndpoints(c => {
        c.Endpoints.RoutePrefix = "api";
        c.Endpoints.Configurator = endpoints => {
            //endpoints.DontThrowIfValidationFails();
            //endpoints.PreProcessor<>();
        };
    })
    .UseSwaggerGen();

app.Run();