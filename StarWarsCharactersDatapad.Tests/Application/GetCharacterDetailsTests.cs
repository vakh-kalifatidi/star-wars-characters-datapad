using StarWarsCharactersDatapad.Client.Application.DTOs;
using StarWarsCharactersDatapad.Client.Application.UseCases;
using StarWarsCharactersDatapad.Tests.Fakes;

namespace StarWarsCharactersDatapad.Tests.Application;

public sealed class GetCharacterDetailsTests
{

    [Fact]
    public async Task ExecuteAsync_ReturnsCharacterDetails()
    {
        //Config data for the fake SWAPI response
        var characterDetails = new CharacterDetailsDto(
            Id: "1",
            Name: "Luke Skywalker",
            Gender: "male",
            BirthYear: "19BBY",
            Height: "172",
            Mass: "77",
            HairColor: "blond",
            SkinColor: "fair",
            EyeColor: "blue",
            Species: [ new SpeciesDto("Human", "mammal", "sentient", "180", "120", "Galactic Basic") ],
            Homeworld: new PlanetDto("Tatooine", "200000", "desert", "arid", "1", "10465", "23", "304", "1 standard"),
            Films: [ new FilmDto("A New Hope", 4) ],
            Starships: [ new TransportDto("X-wing", "Starfighter", "1", "0", "T-65 X-wing", "Incom Corporation") ],
            Vehicles: []
        );

        var provider = new FakeSwapiDataProvider { CharacterDetails = characterDetails };

        var useCase = new GetCharacterDetails(provider);

        //Execute the use case
        var result = await useCase.ExecuteGetCharacterDetails("1");

        Assert.Equal(characterDetails, result);
        Assert.Equal("1", provider.UsedCharacterId);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsNull_WhenCharacterNotFound()
    {
        //Config provider to return null for character details
        var provider = new FakeSwapiDataProvider { CharacterDetails = null };

        var useCase = new GetCharacterDetails(provider);

        //Execute the use case
        var actual = await useCase.ExecuteGetCharacterDetails("111");

        Assert.Null(actual);
        Assert.Equal("111", provider.UsedCharacterId);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ExecuteAsync_ThrowsArgumentException_WhenIdInvalid(string? id)
    {
        //Config provider
        var provider = new FakeSwapiDataProvider();
        
        var useCase = new GetCharacterDetails(provider);

        //Execute the use case
        //ArgumentNullException inherits from ArgumentException
        //- so we can use ThrowsAnyAsync to catch any ArgumentException or derived exception.
        await Assert.ThrowsAnyAsync<ArgumentException>(() => useCase.ExecuteGetCharacterDetails(id!));

        Assert.Null(provider.UsedCharacterId);
    }

}
