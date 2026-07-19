using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DieselNsteel.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<FareServices>();
await builder.Build().RunAsync();
