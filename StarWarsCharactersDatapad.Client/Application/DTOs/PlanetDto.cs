namespace StarWarsCharactersDatapad.Client.Application.DTOs;

public sealed record PlanetDto(
    string Name,
    string Population,
    string Terrain,
    string Climate,
    string SurfaceWater,
    string Diameter,
    string RotationPeriod,
    string OrbitalPeriod,
    string Gravity
);
