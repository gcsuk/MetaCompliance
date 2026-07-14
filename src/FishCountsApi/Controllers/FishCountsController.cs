namespace FishCountsApi.Controllers;

using FishCountsApi.EcologyApi.Clients;
using FishCountsApi.Handlers;
using FishCountsApi.Mappers;
using FishCountsApi.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("fishCounts")]
public class FishCountsController : ControllerBase
{
    private readonly ILogger<FishCountsController> _logger;
    private readonly IGetFishCountsHandler _getFishCountsHandler;

    public FishCountsController(
        ILogger<FishCountsController> logger,
        IGetFishCountsHandler getFishCountsHandler)
    {
        _logger = logger;
        _getFishCountsHandler = getFishCountsHandler;
    }

    /// <summary>
    /// Gets the number of each type of fish counted and the date of the count
    /// </summary>
    [HttpGet(Name = "GetFishCounts")]
    public async Task<IEnumerable<FishCountModel>> GetFishCounts([FromQuery] GetFishCountsQueryModel query)
    {
        return await _getFishCountsHandler.Handle(query);
    }
}
