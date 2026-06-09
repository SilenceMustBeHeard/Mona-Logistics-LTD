using Mona_Logistics_LTD.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Models.Loads;

public class Cargo : BaseDeletableEntity
{ 
   

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double WeightKg { get; set; }

    public double VolumeM3 { get; set; }

    public bool IsFragile { get; set; }

    public bool RequiresCooling { get; set; }

    public Guid ShipmentId { get; set; }

    public Shipment Shipment { get; set; } = null!;
}