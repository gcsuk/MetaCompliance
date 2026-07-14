using FishCountsApi.EcologyApi.Models;
using FishCountsApi.Mappers;
using FishCountsApi.Models;

namespace FishCountsUnitTests.Mappers;

public class FishCountModelMapperTests
{
    [Fact]
    public void FromObservationResponseModel_MapsToFishCountModel_WhenLabelIsSet()
    {
        // Arrange
        var date = new DateOnly(2022, 1, 2);
        var input = new ObservationResponseModel(
            Result_value: 10,
            Ultimate_feature_of_interest_label: "Fish",
            Date: date);
        var expected = new FishCountModel(Name: "Fish", Count: 10, Date: date);

        // Act
        var result = FishCountModelMapper.FromObservationResponseModel(input);

        // Assert
        Assert.Equivalent(expected, result);
    }

    [Fact]
    public void FromObservationResponseModel_MapsToUnknownName_WhenLabelIsNullOrWhiteSpace()
    {
        // Arrange
        var date = new DateOnly(2022, 1, 2);
        var input = new ObservationResponseModel(
            Result_value: 5,
            Ultimate_feature_of_interest_label: null!,
            Date: date);
        var expected = new FishCountModel(Name: "Unknown", Count: 5, Date: date);

        // Act
        var result = FishCountModelMapper.FromObservationResponseModel(input);

        // Assert
        Assert.Equivalent(expected, result);
    }
}
