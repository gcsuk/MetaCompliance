namespace FishCountsApi.EcologyApi.Models;

public record ObservationResponseModel(
    int Result_value,
    string Ultimate_feature_of_interest_label,
    DateOnly Date);
