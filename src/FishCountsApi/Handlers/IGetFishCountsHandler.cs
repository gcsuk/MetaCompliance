using FishCountsApi.Models;

namespace FishCountsApi.Handlers
{
    public interface IGetFishCountsHandler
    {
        Task<IEnumerable<FishCountModel>> Handle(GetFishCountsQueryModel query);
    }
}