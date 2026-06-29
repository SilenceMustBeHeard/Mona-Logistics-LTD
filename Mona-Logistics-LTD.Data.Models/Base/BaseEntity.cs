using System.ComponentModel.DataAnnotations;

namespace Mona_Logistics_LTD.Data.Models.Base;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}