using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Data.Models.Routes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Models.Trucks;

public class Truck : BaseDeletableEntity
{


    public string RegistrationNumber { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public VehicleType VehicleType { get; set; }

    public double MaxWeightKg { get; set; }

    public double MaxVolumeM3 { get; set; }

    public bool IsAvailable { get; set; }

    public ICollection<Trip> Trips { get; set; }
        = new HashSet<Trip>();
}