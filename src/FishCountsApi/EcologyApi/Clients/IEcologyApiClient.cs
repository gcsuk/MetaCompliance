using FishCountsApi.EcologyApi.Models;

namespace FishCountsApi.EcologyApi.Clients;

public interface IEcologyApiClient
{
    Task<List<ObservationResponseModel>> GetObservations(GetObservationsQueryModel query);
}
