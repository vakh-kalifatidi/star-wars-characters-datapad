namespace StarWarsCharactersDatapad.Client.Application.DTOs;

public sealed record TransportDto(
    string Name,
    string Classification,
    string Crew,
    string Passengers,
    string Model,
    string Manufacturer
);

