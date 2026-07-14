namespace FishCountsUnitTests.EcologyApi.Clients;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FishCountsApi.EcologyApi.Clients;
using FishCountsApi.EcologyApi.Models;
using NSubstitute;
using RichardSzalay.MockHttp;
using Xunit;

public class EcologyApiClientTests
{
    private readonly MockHttpMessageHandler _mockHttp;
    private readonly HttpClient _httpClient;
    private readonly EcologyApiClient _ecologyApiClient;

    public EcologyApiClientTests()
    {
        _mockHttp = new MockHttpMessageHandler();
        _httpClient = new HttpClient(_mockHttp) { BaseAddress = new Uri("http://localhost") };
        _ecologyApiClient = new EcologyApiClient(_httpClient);
    }

    [Fact]
    public async Task GetObservations_ReturnsExpectedResult_WhenQueryIsValid()
    {
        // Arrange
        var query = new GetObservationsQueryModel(Date_from: "2021-01-02", Date_to: "2022-03-04");

        var expectedResponse = new List<ObservationResponseModel>
        {
            new(Result_value: 15, Ultimate_feature_of_interest_label: "Fish1", Date: new DateOnly(2022, 2, 3)),
            new(Result_value: 1, Ultimate_feature_of_interest_label: "Fish2", Date: new DateOnly(2022, 2, 4)),
        };

        var responseContent = JsonSerializer.Serialize(expectedResponse);

        _mockHttp.When("http://localhost/observations?date_from=2021-01-02&date_to=2022-03-04").Respond("application/json", responseContent);

        // Act
        var result = await _ecologyApiClient.GetObservations(query);

        // Assert
        Assert.Equal(expectedResponse, result);
    }

    [Fact]
    public async Task GetObservations_ThrowsException_WhenApiReturnsNonSuccessStatusCode()
    {
        // Arrange
        var query = new GetObservationsQueryModel(Date_from: "2021-01-02", Date_to: "2022-03-04");

        _mockHttp.When("http://localhost/observations?date_from=2021-01-02&date_to=2022-03-04").Respond(HttpStatusCode.BadRequest);

        // Act and Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => _ecologyApiClient.GetObservations(query));
        Assert.StartsWith("Failed to fetch observations:", ex.Message);
        Assert.Contains("StatusCode: 400", ex.Message);
    }

    [Fact]
    public async Task GetObservations_ThrowsException_WhenJsonDeserialisationFails()
    {
        // Arrange
        var query = new GetObservationsQueryModel(Date_from: "2021-01-02", Date_to: "2022-03-04");

        _mockHttp.When("http://localhost/observations?date_from=2021-01-02&date_to=2022-03-04").Respond("application/json", "badjson");

        // Act and Assert
        var ex = await Assert.ThrowsAsync<JsonException>(() => _ecologyApiClient.GetObservations(query));
    }
}
