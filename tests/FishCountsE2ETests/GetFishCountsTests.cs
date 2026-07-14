using System.Net;
using System.Net.Http.Json;

namespace FishCountsE2ETests;

public class GetFishCountsTests
{
    private record ResponseModel(string Name, int Count, string Date);

    private record ErrorModel(Dictionary<string, List<string>> Errors);

    [Fact]
    public async void GetFishCounts_ReturnsExpectedItems_WhenDateRangeSpecified()
    {
        var response = await Client.Instance.GetAsync("fishCounts?startDate=2023-09-11&endDate=2023-09-11");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadFromJsonAsync<List<ResponseModel>>();

        var expectedContent = new List<ResponseModel>()
        {
            new(Name: "Flounder", Count: 3, Date: "2023-09-11"),
            new(Name: "Greater sandeel", Count: 2, Date: "2023-09-11"),
            new(Name: "Herring", Count: 2750, Date: "2023-09-11"),
        };

        Assert.NotNull(content);
        Assert.Equal(expectedContent.Count, content.Count);
        Assert.Equivalent(expectedContent, content);
    }

    [Fact]
    public async void GetFishCounts_ReturnsClientError_WhenStartDateOmitted()
    {
        var response = await Client.Instance.GetAsync("fishCounts?endDate=2023-09-11");
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var content = await response.Content.ReadFromJsonAsync<ErrorModel>();

        var expectedContent = new ErrorModel(new Dictionary<string, List<string>>
        {
            { "StartDate", new() { "The StartDate field is required." } }
        });

        Assert.NotNull(content);
        Assert.Equivalent(expectedContent, content);
    }

    [Fact]
    public async void GetFishCounts_ReturnsClientError_WhenEndDateOmitted()
    {
        var response = await Client.Instance.GetAsync("fishCounts?startDate=2023-09-11");
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var content = await response.Content.ReadFromJsonAsync<ErrorModel>();

        var expectedContent = new ErrorModel(new Dictionary<string, List<string>>
        {
            { "EndDate", new() { "The EndDate field is required." } }
        });

        Assert.NotNull(content);
        Assert.Equivalent(expectedContent, content);
    }
}