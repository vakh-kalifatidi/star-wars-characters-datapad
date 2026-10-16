namespace StarWarsCharactersDatapad.Client.Application.DTOs;

public sealed record SpeciesDto(
    string Name,
    string Classification,
    string Designation,
    string AverageHeight,
    string AverageLifespan,
    string Language
);
