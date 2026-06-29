using Mona_Logistics_LTD.Data.Models.Loads;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mona_Logistics_LTD.Data.Models.Base;

public class Invoice : BaseDeletableEntity
{
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    public DateTime IssuedOn { get; set; }

    public bool IsPaid { get; set; }

    public Guid ShipmentId { get; set; }

    public Shipment Shipment { get; set; } = null!;
}