using System.Text.Json.Serialization;

namespace StarWarsCharactersDatapad.Client.Infrastructure.Models;

public sealed class SwapiFilm
{
    public string? Title { get; set; }

    [JsonPropertyName("episode_id")]
    public int EpisodeId { get; set; }
}
