namespace FishCountsApi.Models;

/// <summary>
/// Holds the number of fish counted on a specific date
/// </summary>
/// <param name="Name">The name of the type of fish</param>
/// <param name="Count">The number of fish counted</param>
/// <param name="Date">The date of the count</param>
public record FishCountModel(
    string Name,
    int Count,
    DateOnly Date);
