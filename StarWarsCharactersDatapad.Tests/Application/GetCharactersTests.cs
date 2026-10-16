using StarWarsCharactersDatapad.Client.Application.UseCases;
using StarWarsCharactersDatapad.Tests.Fakes;

namespace StarWarsCharactersDatapad.Tests.Application;

public sealed class GetCharactersTests
{
    [Fact]
    public async Task ExecuteAsync_ReturnsCharacters()
    {
        //Config Fake SWAPI response
        var provider = new FakeSwapiDataProvider
        {
            //Set up the fake data to be returned by the provider
            Characters = [ new("1", "Luke Skywalker"), new("2", "C-3PO") ]
        };

        var useCase = new GetCharacters(provider);

        //Execute the use case
        var result = await useCase.ExecuteGetCharacters();

        Assert.Equal(2, result.Count);
        Assert.Equal("Luke Skywalker", result[0].Name);
    }
}
