namespace FishCountsApi.EcologyApi.Clients;

using FishCountsApi.EcologyApi.Models;
using Microsoft.AspNetCore.Http.Extensions;

public class EcologyApiClient : IEcologyApiClient
{
    private readonly HttpClient _httpClient;

    public EcologyApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ObservationResponseModel>> GetObservations(GetObservationsQueryModel query)
    {
        var queryBuilder = new QueryBuilder
        {
            { "property_id", "http://environment.data.gov.uk/ecology/def/fish/TotalCount" },
            { "site_id", "http://environment.data.gov.uk/ecology/site/fish/26498" },
            { "site_id", "http://environment.data.gov.uk/ecology/site/fish/20636" },
            { "site_id", "http://environment.data.gov.uk/ecology/site/fish/20264" },
            { "site_id", "http://environment.data.gov.uk/ecology/site/fish/20696" },
            { "date_from", query.Date_from },
            { "date_to", query.Date_to },
            { "skip", query.Offset.ToString() },
            { "take", query.Limit.ToString() },
        };

        var response = await _httpClient.GetAsync($"observations{queryBuilder}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to fetch observations: {response}");
        }

        var result = await response.Content.ReadFromJsonAsync<List<ObservationResponseModel>>();
        if (result is null)
        {
            throw new Exception("Failed to parse observations");
        }

        return result;
    }
}
