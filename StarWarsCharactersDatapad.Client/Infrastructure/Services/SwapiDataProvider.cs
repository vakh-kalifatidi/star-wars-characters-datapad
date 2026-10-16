using StarWarsCharactersDatapad.Client.Application.Abstractions;
using StarWarsCharactersDatapad.Client.Application.DTOs;
using StarWarsCharactersDatapad.Client.Infrastructure.Models;
using System.Net;
using System.Net.Http.Json;

using static StarWarsCharactersDatapad.Client.Infrastructure.Helpers.SwapiDataHelper;

namespace StarWarsCharactersDatapad.Client.Infrastructure.Services;

public sealed class SwapiDataProvider(HttpClient httpClient) : IStarWarsDataProvider
{
    public async Task<IReadOnlyList<CharacterDto>> GetCharactersAsync(string? search = null,
        CancellationToken cancellationToken = default)
    {
        //we will set the Base URL in the Program.cs file, so we can just use the relative path here
        var characters = await httpClient.GetFromJsonAsync<List<SwapiCharacter>>("people", JsonOptions, cancellationToken);

        return characters is null
            ? []
            : characters.Where(c => !string.IsNullOrWhiteSpace(c.Name) && !string.IsNullOrWhiteSpace(c.Url))
                .Where(c => string.IsNullOrWhiteSpace(search) || c.Name!.Contains(search.Trim(), StringComparison.OrdinalIgnoreCase))
                .Select(c => new CharacterDto(ExtractId(c.Url!), c.Name!))
                .ToList();
    }

    public async Task<CharacterDetailsDto?> GetCharacterDetailsAsync(string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        if (!int.TryParse(id, out var characterId) || characterId <= 0)
            throw new ArgumentException("Character ID must be a positive integer.", nameof(id));

        using var response = await httpClient.GetAsync($"people/{characterId}",
            cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        var character = await response.Content.ReadFromJsonAsync<SwapiCharacter>(JsonOptions, cancellationToken);

        if (character is null)
            return null;

        var species = await GetSpeciesAsync(character.Species,
            cancellationToken);

        var homeworld = await GetHomeworldAsync(character.Homeworld,
            cancellationToken);

        var films = await GetFilmsAsync(character.Films,
            cancellationToken);

        var starships = await GetTransportDevicesAsync(character.Starships,
            cancellationToken);

        var vehicles = await GetTransportDevicesAsync(character.Vehicles,
            cancellationToken);

        return new CharacterDetailsDto(
            id,
            Normalize(character.Name),
            Normalize(character.Gender),
            Normalize(character.BirthYear),
            Normalize(character.Height),
            Normalize(character.Mass),
            Normalize(character.HairColor),
            Normalize(character.SkinColor),
            Normalize(character.EyeColor),
            species,
            homeworld,
            films,
            starships,
            vehicles);
    }


    private async Task<IReadOnlyList<TransportDto>> GetTransportDevicesAsync(IEnumerable<string>? urls,
        CancellationToken cancellationToken)
    {
        var transports = await GetResourcesAsync<SwapiTransport>(urls, cancellationToken);

        return transports.Select(t => new TransportDto(
                Normalize(t.Name),
                FirstKnown(t.StarshipClass ?? t.VehicleClass ?? t.Classification),
                Normalize(t.Crew),
                Normalize(t.Passengers),
                Normalize(t.Model),
                Normalize(t.Manufacturer)))
            .ToList();
    }


    private async Task<IReadOnlyList<FilmDto>> GetFilmsAsync(IEnumerable<string>? urls,
        CancellationToken cancellationToken)
    {
        var films = await GetResourcesAsync<SwapiFilm>(urls, cancellationToken);

        return films.Select(f => new FilmDto(Normalize(f.Title), f.EpisodeId)).ToList();
    }


    private async Task<PlanetDto?> GetHomeworldAsync(string? url,
        CancellationToken cancellationToken)
    {
        var planet = await GetResourceAsync<SwapiPlanet>(url, cancellationToken);

        return planet is null
            ? null
            : new PlanetDto(
                Normalize(planet.Name),
                Normalize(planet.Population),
                Normalize(planet.Terrain),
                Normalize(planet.Climate),
                Normalize(planet.SurfaceWater),
                Normalize(planet.Diameter),
                Normalize(planet.RotationPeriod),
                Normalize(planet.OrbitalPeriod),
                Normalize(planet.Gravity));
    }


    private async Task<IReadOnlyList<SpeciesDto>> GetSpeciesAsync(IEnumerable<string>? urls,
        CancellationToken cancellationToken)
    {
        var species = await GetResourcesAsync<SwapiSpecies>(
            urls,
            cancellationToken);

        return species.Select(s => new SpeciesDto(
                Normalize(s.Name),
                Normalize(s.Classification),
                Normalize(s.Designation),
                Normalize(s.AverageHeight),
                Normalize(s.AverageLifespan),
                Normalize(s.Language)))
            .ToList();
    }

    private async Task<IReadOnlyList<T>> GetResourcesAsync<T>(IEnumerable<string>? urls,
        CancellationToken cancellationToken)
    {
        if (urls is null)
            return [];

        var tasks = urls.Select(url => GetResourceAsync<T>(url, cancellationToken));

        var results = await Task.WhenAll(tasks);

        return results.Where(x => x is not null).Select(x => x!).ToList();
    }


    private async Task<T?> GetResourceAsync<T>(string? url, 
        CancellationToken cancellationToken)
        => string.IsNullOrWhiteSpace(url)
        ? default
        : await httpClient.GetFromJsonAsync<T>(url, JsonOptions, cancellationToken);

}
