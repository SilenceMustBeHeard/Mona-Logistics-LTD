using Mona_Logistics_LTD.Data.Models.Base;

namespace Mona_Logistics_LTD.Data.Models.Loads;

public class LoadRequestDocument : BaseDeletableEntity
{
    public Guid LoadRequestId { get; set; }
    public virtual LoadRequest LoadRequest { get; set; } = null!;

    public string FileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    // e.g., "pdf", "jpg", "png"
    public string FileType { get; set; } = null!; 
    // in bytes
    public long FileSize { get; set; } 

    public string? Description { get; set; }
}