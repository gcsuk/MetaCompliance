using FishCountsApi.EcologyApi.Clients;
using FishCountsApi.Mappers;
using FishCountsApi.Models;

namespace FishCountsApi.Handlers;

public class GetFishCountsHandler : IGetFishCountsHandler
{
    private readonly IEcologyApiClient _ecologyApiClient;

    public GetFishCountsHandler(IEcologyApiClient ecologyApiClient)
    {
        _ecologyApiClient = ecologyApiClient;
    }

    public async Task<IEnumerable<FishCountModel>> Handle(GetFishCountsQueryModel query)
    {
        var ecologyQuery = GetFishCountsQueryModelMapper.ToGetObservationsQueryModel(query);
        var observations = await _ecologyApiClient.GetObservations(ecologyQuery);
        return observations.Select(FishCountModelMapper.FromObservationResponseModel).ToList();
    }
}
