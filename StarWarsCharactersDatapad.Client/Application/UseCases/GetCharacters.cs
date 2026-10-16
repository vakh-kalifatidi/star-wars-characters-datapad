using StarWarsCharactersDatapad.Client.Application.Abstractions;
using StarWarsCharactersDatapad.Client.Application.DTOs;

namespace StarWarsCharactersDatapad.Client.Application.UseCases;

public sealed class GetCharacters(IStarWarsDataProvider dataProvider)
{
    //No awaiting - return the Task directly - the caller can await that task.
    public Task<IReadOnlyList<CharacterDto>> ExecuteGetCharacters(string? search = null,
        CancellationToken cancellationToken = default) =>
        dataProvider.GetCharactersAsync(search, 
            cancellationToken);
}
