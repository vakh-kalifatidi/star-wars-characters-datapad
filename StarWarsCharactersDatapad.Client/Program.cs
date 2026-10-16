using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StarWarsCharactersDatapad.Client;
using StarWarsCharactersDatapad.Client.Application.Abstractions;
using StarWarsCharactersDatapad.Client.Application.UseCases;
using StarWarsCharactersDatapad.Client.Infrastructure.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


//Application use cases
builder.Services.AddScoped<GetCharacters>();
builder.Services.AddScoped<GetCharacterDetails>();

//Infrastructure
builder.Services.AddScoped<IStarWarsDataProvider>(dp => new SwapiDataProvider(
    new HttpClient{
        BaseAddress = new Uri("https://swapi.info/api/"),
        Timeout = TimeSpan.FromSeconds(30)
    }));

await builder.Build().RunAsync();
