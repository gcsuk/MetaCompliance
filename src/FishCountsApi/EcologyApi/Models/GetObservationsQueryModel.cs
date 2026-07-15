namespace FishCountsApi.EcologyApi.Models;

public record GetObservationsQueryModel(
    string Date_from,
    string Date_to,
    int Offset,
    int Limit);
