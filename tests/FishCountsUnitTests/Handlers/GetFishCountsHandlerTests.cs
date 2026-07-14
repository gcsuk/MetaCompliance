namespace FishCountsUnitTests.Handlers;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FishCountsApi.EcologyApi.Clients;
using FishCountsApi.EcologyApi.Models;
using FishCountsApi.Handlers;
using FishCountsApi.Mappers;
using FishCountsApi.Models;
using NSubstitute;
using Xunit;

public class GetFishCountsHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnFishCountModels()
    {
        // Arrange
        var query = new GetFishCountsQueryModel()
        {
            StartDate = new DateOnly(2021, 2, 3),
            EndDate = new DateOnly(2023, 11, 12),
        };
        var ecologyQuery = GetFishCountsQueryModelMapper.ToGetObservationsQueryModel(query);

        var observations = new List<ObservationResponseModel>
        {
            new ObservationResponseModel(Result_value: 5, Ultimate_feature_of_interest_label: "Fish1", Date: new DateOnly(2022, 1, 2)),
            new ObservationResponseModel(Result_value: 10, Ultimate_feature_of_interest_label: "Fish2", Date: new DateOnly(2022, 2, 3)),
            new ObservationResponseModel(Result_value: 50, Ultimate_feature_of_interest_label: "Fish3", Date: new DateOnly(2022, 3, 4)),
        };

        var ecologyApiClient = Substitute.For<IEcologyApiClient>();
        ecologyApiClient.GetObservations(ecologyQuery).Returns(observations);

        var handler = new GetFishCountsHandler(ecologyApiClient);

        var expected = observations.Select(FishCountModelMapper.FromObservationResponseModel).ToList();

        // Act
        var result = await handler.Handle(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equivalent(expected, result);
    }
}
