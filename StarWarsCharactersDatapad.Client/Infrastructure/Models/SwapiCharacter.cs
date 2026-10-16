using System.Text.Json.Serialization;

namespace StarWarsCharactersDatapad.Client.Infrastructure.Models;

public sealed class SwapiCharacter
{
    public string? Name { get; set; }
    public string? Url { get; set; }

    public string? Gender { get; set; }

    [JsonPropertyName("birth_year")]
    public string? BirthYear { get; set; }

    public string? Height { get; set; }
    public string? Mass { get; set; }

    [JsonPropertyName("hair_color")]
    public string? HairColor { get; set; }

    [JsonPropertyName("skin_color")]
    public string? SkinColor { get; set; }

    [JsonPropertyName("eye_color")]
    public string? EyeColor { get; set; }

    public string? Homeworld { get; set; }

    //SWAPI returns only the URLs of the related resources,
    //so we will store them as strings and fetch the actual data when needed.
    public List<string>? Species { get; set; }
    public List<string>? Films { get; set; }
    public List<string>? Starships { get; set; }
    public List<string>? Vehicles { get; set; }
}
