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
            new(Name: "Flounder", Count: 4, Date: "2023-09-11"),
            new(Name: "Greater sandeel", Count: 3, Date: "2023-09-11"),
            new(Name: "Herring", Count: 623, Date: "2023-09-11"),
            new(Name: "Flounder", Count: 3, Date: "2023-09-11"),
            new(Name: "Greater sandeel", Count: 2, Date: "2023-09-11"),
            new(Name: "Herring", Count: 2750, Date: "2023-09-11"),
        };

        Assert.NotNull(content);
        Assert.Equal(expectedContent.Count, content.Count);
        Assert.Equivalent(expectedContent, content);
    }

    [Fact]
    public async void GetFishCounts_ReturnsFewerItems_WhenLimitSpecified()
    {
        var response = await Client.Instance.GetAsync("fishCounts?startDate=2023-09-11&endDate=2023-09-11&limit=1");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadFromJsonAsync<List<ResponseModel>>();

        Assert.NotNull(content);
        Assert.Single(content);
    }

    [Fact]
    public async void GetFishCounts_ReturnsDifferentItem_WhenOffsetSpecified()
    {
        var firstPageResponse = await Client.Instance.GetAsync("fishCounts?startDate=2023-09-11&endDate=2023-09-11&offset=0&limit=1");
        var secondPageResponse = await Client.Instance.GetAsync("fishCounts?startDate=2023-09-11&endDate=2023-09-11&offset=1&limit=1");

        Assert.Equal(HttpStatusCode.OK, firstPageResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, secondPageResponse.StatusCode);

        var firstPageContent = await firstPageResponse.Content.ReadFromJsonAsync<List<ResponseModel>>();
        var secondPageContent = await secondPageResponse.Content.ReadFromJsonAsync<List<ResponseModel>>();

        Assert.NotNull(firstPageContent);
        Assert.NotNull(secondPageContent);
        Assert.Single(firstPageContent);
        Assert.Single(secondPageContent);
        Assert.NotEqual(firstPageContent.Single(), secondPageContent.Single());
    }

    [Fact]
    public async void GetFishCounts_ReturnsClientError_WhenOffsetIsNegative()
    {
        var response = await Client.Instance.GetAsync("fishCounts?startDate=2023-09-11&endDate=2023-09-11&offset=-1");
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var content = await response.Content.ReadFromJsonAsync<ErrorModel>();

        var expectedContent = new ErrorModel(new Dictionary<string, List<string>>
        {
            { "Offset", new() { "The field Offset must be between 0 and 2147483647." } }
        });

        Assert.NotNull(content);
        Assert.Equivalent(expectedContent, content);
    }

    [Fact]
    public async void GetFishCounts_ReturnsClientError_WhenLimitIsNegative()
    {
        var response = await Client.Instance.GetAsync("fishCounts?startDate=2023-09-11&endDate=2023-09-11&limit=-1");
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var content = await response.Content.ReadFromJsonAsync<ErrorModel>();

        var expectedContent = new ErrorModel(new Dictionary<string, List<string>>
        {
            { "Limit", new() { "The field Limit must be between 0 and 2147483647." } }
        });

        Assert.NotNull(content);
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