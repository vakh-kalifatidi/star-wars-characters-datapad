using StarWarsCharactersDatapad.Client.Infrastructure.Services;
using System.Net;

using StarWarsCharactersDatapad.Tests.Testing;

namespace StarWarsCharactersDatapad.Tests.Infrastructure;

public sealed class SwapiDataProviderTests
{
    [Fact]
    public async Task GetCharacterDetailsAsync_ReturnsNull_WhenNotFound()
    {
        var handler = new FakeHttpMessageHandler(HttpStatusCode.NotFound);

        using var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://swapi.info/api/")
        };

        var provider = new SwapiDataProvider(httpClient);

        var result = await provider.GetCharacterDetailsAsync("999");

        Assert.Null(result);
    }

    private sealed class FakeHttpMessageHandler(HttpStatusCode statusCode) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(statusCode));
        }
    }

    [Fact]
    public async Task GetCharactersAsync_MapsAndFiltersCharacters()
    {
        const string json = """
        [
          {
            "name": "Luke Skywalker",
            "url": "https://swapi.dev/api/people/1/"
          },
          {
            "name": "Leia Organa",
            "url": "https://swapi.dev/api/people/5/"
          },
          {
            "name": "C-3PO",
            "url": "https://swapi.dev/api/people/2/"
          }
        ]
        """;

        using var httpClient = new HttpClient(new JsonHttpMessageHandler(json))
        {
            BaseAddress = new Uri("https://swapi.info/api/")
        };

        var provider = new SwapiDataProvider(httpClient);

        var result = await provider.GetCharactersAsync("luKE");

        var character = Assert.Single(result);

        Assert.Equal("1", character.Id);
        Assert.Equal("Luke Skywalker", character.Name);
    }
}