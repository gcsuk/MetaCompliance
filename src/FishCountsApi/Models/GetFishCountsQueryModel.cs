using System.ComponentModel.DataAnnotations;

namespace FishCountsApi.Models;

public class GetFishCountsQueryModel
{
    /// <summary>
    /// The start date of the period to get fish counts for in the form "yyyy-MM-dd"
    /// </summary>
    /// <example>2023-01-01</example>
    [Required]
    public DateOnly? StartDate { get; set; }

    /// <summary>
    /// The end date of the period to get fish counts for in the form "yyyy-MM-dd"
    /// </summary>
    /// <example>2023-12-31</example>
    [Required]
    public DateOnly? EndDate { get; set; }
}
