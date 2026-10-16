using StarWarsCharactersDatapad.Client.Application.Abstractions;
using StarWarsCharactersDatapad.Client.Application.DTOs;

namespace StarWarsCharactersDatapad.Client.Application.UseCases;

public sealed class GetCharacterDetails(IStarWarsDataProvider dataProvider)
{
    //No awaiting - return the Task directly - the caller can await that task.
    public Task<CharacterDetailsDto?> ExecuteGetCharacterDetails(string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        return dataProvider.GetCharacterDetailsAsync(id,
            cancellationToken);
    }
}
