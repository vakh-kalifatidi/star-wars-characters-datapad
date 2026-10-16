namespace StarWarsCharactersDatapad.Client.Application.DTOs;

public sealed record CharacterDetailsDto(
    string Id,
    string Name,
    string Gender, 
    string BirthYear,
    string Height,
    string Mass,
    string HairColor,
    string SkinColor,
    string EyeColor,
    IReadOnlyList<SpeciesDto>? Species, 
    PlanetDto? Homeworld,
    IReadOnlyList<FilmDto> Films,
    IReadOnlyList<TransportDto> Starships,
    IReadOnlyList<TransportDto> Vehicles
);
