using FishCountsApi.EcologyApi.Models;
using FishCountsApi.Mappers;
using FishCountsApi.Models;

namespace FishCountsUnitTests.Mappers;

public class GetFishCountsQueryModelMapperTests
{
    [Fact]
    public void ToGetObservationsQueryModel_MapsInputToGetObservationsQueryModel()
    {
        // Arrange
        var input = new GetFishCountsQueryModel
        {
            StartDate = new DateOnly(2022, 1, 2),
            EndDate = new DateOnly(2022, 1, 31),
            Offset = 20,
            Limit = 10
        };
        var expected = new GetObservationsQueryModel(Date_from: "2022-01-02", Date_to: "2022-01-31", Offset: 20, Limit: 10);

        // Act
        var result = GetFishCountsQueryModelMapper.ToGetObservationsQueryModel(input);

        // Assert
        Assert.Equivalent(expected, result);
    }

    [Fact]
    public void ToGetObservationsQueryModel_UsesDefaultOffsetAndLimit_WhenNotSpecified()
    {
        // Arrange
        var input = new GetFishCountsQueryModel
        {
            StartDate = new DateOnly(2022, 1, 2),
            EndDate = new DateOnly(2022, 1, 31)
        };
        var expected = new GetObservationsQueryModel(Date_from: "2022-01-02", Date_to: "2022-01-31", Offset: 0, Limit: 50);

        // Act
        var result = GetFishCountsQueryModelMapper.ToGetObservationsQueryModel(input);

        // Assert
        Assert.Equivalent(expected, result);
    }
}
