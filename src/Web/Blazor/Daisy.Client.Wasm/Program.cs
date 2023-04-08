using Blazored.LocalStorage;
using BlazorStrap;
using Daisy.Client.Wasm;
using Daisy.Client.Wasm.AuthProviders;
using Daisy.Client.Wasm.Extensions;
using Daisy.Client.Wasm.Handlers.AuthHandlers;
using Daisy.Domain.Models;
using Daisy.Infrastructure.Context;
using Daisy.Infrastructure.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Radzen;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7060/") });
builder.Services.AddHttpContextAccessor();
builder.Services.AddLogging(options =>
{

});

builder.Services.AddBlazorWasm();

builder.Services.AddBlazorStrap();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync(); 


