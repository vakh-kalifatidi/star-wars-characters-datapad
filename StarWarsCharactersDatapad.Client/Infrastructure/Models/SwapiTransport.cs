using System.Text.Json.Serialization;

namespace StarWarsCharactersDatapad.Client.Infrastructure.Models;


// Represents a data from the Star Wars API (SWAPI)
// .NET standard is to use PascalCase for property names, but SWAPI uses snake_case,
// -so we use JsonPropertyName to map the JSON properties to the .NET properties.
public sealed class SwapiTransport
{
    public string? Name { get; set; }
    public string? Classification { get; set; }
    public string? Crew { get; set; }
    public string? Passengers { get; set; }
    public string? Model { get; set; }
    public string? Manufacturer { get; set; }

    [JsonPropertyName("starship_class")]
    public string? StarshipClass { get; set; }

    [JsonPropertyName("vehicle_class")]
    public string? VehicleClass { get; set; }
}
