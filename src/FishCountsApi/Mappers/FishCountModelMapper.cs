namespace FishCountsApi.Mappers;

using FishCountsApi.EcologyApi.Models;
using FishCountsApi.Models;

public static class FishCountModelMapper
{
    public static FishCountModel FromObservationResponseModel(ObservationResponseModel input)
    {
        var name = string.IsNullOrWhiteSpace(input.Ultimate_feature_of_interest_label) ? "Unknown" : input.Ultimate_feature_of_interest_label;
        return new FishCountModel(
            Name: name,
            Count: input.Result_value,
            Date: input.Date);
    }
}
