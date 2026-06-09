using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Data.Models.Trucks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Models.Routes;

public class Trip : BaseDeletableEntity
{
   

    public Guid TruckId { get; set; }

    public Truck Truck { get; set; } = null!;

    public Guid DriverId { get; set; }

    public Driver Driver { get; set; } = null!;

    public Guid RouteId { get; set; }

    public Route Route { get; set; } = null!;

    public Guid ShipmentId { get; set; }

    public Shipment Shipment { get; set; } = null!;

    public DateTime DepartureDate { get; set; }

    public DateTime? ArrivalDate { get; set; }
}