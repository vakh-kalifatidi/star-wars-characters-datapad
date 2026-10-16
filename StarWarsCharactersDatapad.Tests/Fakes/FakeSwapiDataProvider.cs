using StarWarsCharactersDatapad.Client.Application.Abstractions;
using StarWarsCharactersDatapad.Client.Application.DTOs;

namespace StarWarsCharactersDatapad.Tests.Fakes;

public sealed class FakeSwapiDataProvider : IStarWarsDataProvider
{
    public IReadOnlyList<CharacterDto> Characters { get; set; } = [];

    public CharacterDetailsDto? CharacterDetails { get; set; }

    public string? UsedSearch { get; private set; }

    public string? UsedCharacterId { get; private set; }


    public Task<IReadOnlyList<CharacterDto>> GetCharactersAsync(string? search = null,
        CancellationToken cancellationToken = default)
    {
        UsedSearch = search;
        return Task.FromResult(Characters);
    }

    public Task<CharacterDetailsDto?> GetCharacterDetailsAsync(string id,
        CancellationToken cancellationToken = default)
    {
        UsedCharacterId = id;
        return Task.FromResult(CharacterDetails);
    }
}
