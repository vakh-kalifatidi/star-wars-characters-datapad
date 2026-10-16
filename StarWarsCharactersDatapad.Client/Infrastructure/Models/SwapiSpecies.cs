using System.Text.Json.Serialization;

namespace StarWarsCharactersDatapad.Client.Infrastructure.Models;

public sealed class SwapiSpecies
{
    public string? Name { get; set; }
    public string? Classification { get; set; }
    public string? Designation { get; set; }

    [JsonPropertyName("average_height")]
    public string? AverageHeight { get; set; }

    [JsonPropertyName("average_lifespan")]
    public string? AverageLifespan { get; set; }

    public string? Language { get; set; }
}
