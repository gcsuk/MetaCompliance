namespace FishCountsApi.Mappers;

using FishCountsApi.EcologyApi.Models;
using FishCountsApi.Models;
using System.Globalization;

public static class GetFishCountsQueryModelMapper
{
    public static GetObservationsQueryModel ToGetObservationsQueryModel(GetFishCountsQueryModel input)
    {
        return new GetObservationsQueryModel(
            Date_from: input.StartDate?.ToString("o", CultureInfo.InvariantCulture) ?? "",
            Date_to: input.EndDate?.ToString("o", CultureInfo.InvariantCulture) ?? "") ;
    }
}
