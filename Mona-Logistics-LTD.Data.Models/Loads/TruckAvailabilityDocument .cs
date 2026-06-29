using Mona_Logistics_LTD.Data.Models.Base;

namespace Mona_Logistics_LTD.Data.Models.Loads;

public class TruckAvailabilityDocument : BaseDeletableEntity
{
    public Guid TruckAvailabilityId { get; set; }
    public virtual TruckAvailability TruckAvailability { get; set; } = null!;

    public string FileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public string FileType { get; set; } = null!;
    public long FileSize { get; set; }
    public string? Description { get; set; }
}