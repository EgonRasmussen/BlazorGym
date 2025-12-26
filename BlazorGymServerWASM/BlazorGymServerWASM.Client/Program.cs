using BlazorGymServerWASM.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services
          .AddSingleton<ISingletonService, WASMSingletonService>()
          .AddTransient<ITransientService, WASMTransientService>()
          .AddScoped<IScopedService, WASMScopedService>();

await builder.Build().RunAsync();
