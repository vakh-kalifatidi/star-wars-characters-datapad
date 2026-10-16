using StarWarsCharactersDatapad.Client.Application.DTOs;

namespace StarWarsCharactersDatapad.Client.Application.Abstractions;

//Application-layer contract — Defines data retrieval operations required by application use cases
//and decouples them from infrastructure implementations and external data sources.
public interface IStarWarsDataProvider
{
    Task<IReadOnlyList<CharacterDto>> GetCharactersAsync(
        string? search = null,
        CancellationToken cancellationToken = default);

    Task<CharacterDetailsDto?> GetCharacterDetailsAsync(
        string id,
        CancellationToken cancellationToken = default);
}

