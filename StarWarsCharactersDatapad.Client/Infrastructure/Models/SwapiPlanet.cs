using System.Text.Json.Serialization;

namespace StarWarsCharactersDatapad.Client.Infrastructure.Models;

public sealed class SwapiPlanet
{
    public string? Name { get; set; }
    public string? Population { get; set; }
    public string? Terrain { get; set; }
    public string? Climate { get; set; }
    public string? Gravity { get; set; }

    [JsonPropertyName("surface_water")]
    public string? SurfaceWater { get; set; }

    public string? Diameter { get; set; }

    [JsonPropertyName("rotation_period")]
    public string? RotationPeriod { get; set; }

    [JsonPropertyName("orbital_period")]
    public string? OrbitalPeriod { get; set; }
}
