using Contracts;
using Blazored.LocalStorage;
using Blazored.Modal;
using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FrontEnd;
using FrontEnd.Infrastructure;
using FrontEnd.Infrastructure.Authentication;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddLogging();
builder.Services.AddScoped<MemoryStorageUtility>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddBlazoredModal();

builder.Services.AddScoped<CookieHandler>();

builder.Services.AddRefitClient<IIdentityApiService>()
    .ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:7050/api"))
    .AddHttpMessageHandler<CookieHandler>();

await builder.Build().RunAsync();