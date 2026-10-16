using System.Text.Json;

namespace StarWarsCharactersDatapad.Client.Infrastructure.Helpers;

internal static class SwapiDataHelper
{
    //SWAPI uses snake_case - CamelCase formating should be used - will handle it via attribs - JSON properties
    public static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public static string ExtractId(string url) => url.TrimEnd('/').Split('/').Last();

    public static string Normalize(string? value) => 
        string.IsNullOrWhiteSpace(value)
        ? "Unknown"
        : value;

    public static string FirstKnown(params string?[] values) =>
    values.FirstOrDefault(v => !string.IsNullOrWhiteSpace(v))
    ?? "Unknown";

}
