# Star Wars Characters Datapad

A standalone **Blazor WebAssembly** application for browsing Star Wars characters and their related information using the [Star Wars API (SWAPI)](https://swapi.info/).

Developed as a take-home exercise for the **LiminalArc Senior Software Engineer** position.

## Overview

The application lets users browse and search characters, select a character, and explore attributes, species, homeworld, films, starships, and vehicles.

The implementation emphasizes separation of concerns, dependency inversion, reusable UI components, testability, and pragmatic integration with an external API.

## Features

- Browse and search Star Wars characters by name (case-insensitive).
- Select a character to view attributes and related information.
- Explore species, homeworld, films, starships, and vehicles where available from SWAPI.
- Display loading, empty, and error states, with Retry actions for failed requests.
- Use a responsive, component-based Blazor WebAssembly interface.

## Technology Stack

| Technology | Purpose |
| --- | --- |
| .NET 10 / C# | Application development |
| Blazor WebAssembly (standalone) | Browser-based user interface |
| .NET dependency injection | Dependency registration and composition |
| `HttpClient` | SWAPI requests |
| `System.Text.Json` | JSON deserialization |
| xUnit | Automated tests |
| Git / GitHub | Source control and submission |

## Architecture

The solution applies **Clean Architecture principles** with a pragmatic, DDD-inspired organization suitable for a read-only application. The architectural boundaries are **logical folders within the Blazor client project**, not separate assemblies.

### Solution Structure

```text
StarWarsCharactersDatapad/
├── StarWarsCharactersDatapad.Client/
│   ├── Application/
│   │   ├── Abstractions/
│   │   ├── DTOs/
│   │   └── UseCases/
│   │   
│   ├── Infrastructure/
│   │   ├── Models/
│   │   ├── Services/
│   │   └── Helpers/
│   ├── Components/
│   ├── Pages/
│   ├── Layout/
│   ├── wwwroot/
│   └── Program.cs
└── StarWarsCharactersDatapad.Tests/
    ├── Application/
    ├── Infrastructure/
    ├── Fakes/
    └── Testing/
```

### Responsibilities

**Application** defines `IStarWarsDataProvider`, application DTOs, and the `GetCharacters` and `GetCharacterDetails` use cases. These use cases are independently testable through the provider abstraction.

**Infrastructure** implements `IStarWarsDataProvider` in `SwapiDataProvider`, deserializes SWAPI responses into external models, resolves linked resources, and maps data to application DTOs.

**Presentation** uses Blazor pages and reusable components to render data and manage selection, search, loading, empty, and error states. The current page injects `IStarWarsDataProvider` directly; the use-case classes remain available and are independently tested. This is a pragmatic choice for the exercise rather than strict enforcement of use-case-only UI access.

**Tests** validate application use-case behavior and infrastructure mapping and HTTP handling using controlled dependencies, without calling the live API.

### Dependency Direction

```text
Presentation ──────► Application contracts / DTOs
                           ▲
                           │ implements
                     Infrastructure
```

The application contract does not depend on a particular HTTP provider. The client composition root registers the implementation through dependency injection.

A separate Domain layer was not introduced: this application primarily reads and displays externally sourced data, with little domain-specific business behavior. Domain entities and rules could be added if the scope expands.

## SWAPI Integration

**Configured API base URL:** `https://swapi.info/api/`

`SwapiDataProvider`:

- Retrieves character lists and individual character details.
- Performs case-insensitive name filtering on the retrieved character list.
- Follows SWAPI URLs for species, homeworld, films, starships, and vehicles.
- Maps external JSON models into application DTOs and normalizes missing text values.
- Supports cancellation tokens and asynchronous HTTP requests.
- Fetches multiple related resources within a category concurrently using `Task.WhenAll`.
- Returns `null` for a character-details HTTP 404 response; other unsuccessful HTTP responses propagate as failures for the UI to handle.

**Source-data limitation:** SWAPI returns an empty `species` array for some characters, including Luke Skywalker. The UI displays **Unknown** rather than inferring a species that the API did not explicitly provide.

## Getting Started

### Prerequisites

- .NET SDK matching the project's target framework (currently .NET 10).
- Visual Studio 2026 with the ASP.NET and web development workload, or another compatible .NET development environment.
- Internet access to SWAPI.

### Clone

```bash
git clone https://github.com/vakh-kalifatidi/Exercise-Blazor-WebAssembly-Standalone-App.git
cd Exercise-Blazor-WebAssembly-Standalone-App
```

### Run

From the repository root:

```bash
dotnet restore
dotnet run --project StarWarsCharactersDatapad.Client
```

Open the local address displayed in the terminal. Alternatively, open the solution in Visual Studio, select `StarWarsCharactersDatapad.Client` as the startup project, and run it.

### Test

```bash
dotnet test
```

The current automated suite contains **9 passing test cases**:

| Area | Tests | Coverage |
| --- | ---: | --- |
| Application | 7 | Character retrieval, search argument forwarding, details retrieval, missing characters, invalid IDs |
| Infrastructure | 2 | Character mapping and case-insensitive filtering; HTTP 404 behavior |

Application tests use `FakeSwapiDataProvider`; Infrastructure tests use simulated HTTP responses. Neither requires a live SWAPI connection.

Blazor component behavior has been verified manually, including searching, empty results, details display, error states, and Retry actions. Automated component tests are not currently included.

## Design Decisions and Tradeoffs

- **Standalone Blazor WebAssembly:** No separate backend or persistence layer was added because the exercise is read-only and SWAPI can be consumed directly from the browser. This keeps the solution simple, but makes the client dependent on external API availability and browser-accessible endpoints.
- **Provider abstraction:** `IStarWarsDataProvider` separates data access contracts from SWAPI-specific integration and enables isolated tests.
- **External models versus application DTOs:** SWAPI response shapes are kept separate from the data contracts consumed by the UI.
- **Read-only collection interfaces:** `IReadOnlyList<T>` communicates collection access without exposing mutation methods through the contract.
- **Reusable components:** Detail cards and related-information sections share presentation patterns rather than duplicating markup.
- **Pragmatic boundaries:** The architecture is intentionally organized within one client project. The UI currently accesses the provider contract directly, while application use cases demonstrate a separately testable application boundary.
- **Cancellation and error handling:** Search and detail requests use cancellation to reduce stale updates; UI error states provide Retry actions. A failed related-resource request can still prevent a complete details result.

## Potential Enhancements

- Add automated Blazor component tests for search, selection, loading, errors, and Retry behavior.
- Improve handling of partial failures when retrieving linked resources.
- Add response caching and more detailed diagnostics/observability.
- Introduce structured application results and failure classification if requirements become more complex.
- Consider pagination or server-side search if a future SWAPI source requires it.

These are future improvements, not requirements for the current implementation.

## AI Assistance

AI assistance was used during development for implementation guidance, code review, test-case suggestions, and README drafting/refinement. The application behavior was checked manually, API responses were verified using Postman, and the automated test suite was executed in Visual Studio. Final implementation and submission decisions remain the developer's responsibility.

## Acknowledgments

Character and related data are provided by [SWAPI](https://swapi.info/). This independent technical exercise is not affiliated with Lucasfilm or Disney.
